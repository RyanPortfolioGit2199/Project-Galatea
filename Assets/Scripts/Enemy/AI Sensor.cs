using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AISensor : MonoBehaviour
{
    [SerializeField] float distance = 10f;
    [SerializeField] float enemyDistance = 10f;
    [SerializeField] float angle = 30f;
    [SerializeField] float height = 1.0f;
    [SerializeField] Color meshColor = Color.yellow;
    [SerializeField] bool showAISensor;
    [SerializeField] float scanFrequency = 0.5f;
    float scanInterval;
    float scanTimer;
    [SerializeField] List<GameObject> Player = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();
    
    [SerializeField] LayerMask playerLayers;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] LayerMask occlusionLayer;
    public Vector3 PlayersLocation {get; private set;}
    public Vector3 EnemyLocation{get; private set;}
    public bool PlayerInSight{get; private set;}

    public bool fleeAway{get; private set;}

    public bool pathObstructed{get; private set;}
    private NavMeshHit obstacleHit;
    NavMeshAgent agent;
    [SerializeField] float maxDistance;
    int playerCount;
    int enemyCount;
    Collider[] playerCollider = new Collider[2];
    Collider[] enemyColliders = new Collider[10];


    Mesh aiFOVCone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        scanInterval = 1.0f/scanFrequency;
        PlayerInSight = false;
    }

    // Update is called once per frame
    void Update()
    {
        scanTimer -= Time.deltaTime;
        if(scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
            EnemyScan();
            if(!agent.enabled){ return;}
            PathObstructedCheck();
        }
        
    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh(); // creating a new mesh instance here

        int segments = 10;

        //The shape is hard-coded to consist of 8 triangles (2 for each rectangular side, 1 for the top, 1 for the bottom).
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3; // each triangle will have 3 vertices

        //Since every triangle needs 3 points, we need an array of 24 vertices ($8 \times 3 = 24$).
        Vector3[] vertices = new Vector3[numVertices]; 
        int[] triangles = new int[numVertices];
        //These initialize the data structures. Notice the triangle array is the same size as the vertex array; this is because this specific script doesn't share vertices between triangles (which makes the edges look sharp).

        

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        //The next block fills the vertices array. It defines triangles in groups of three. The order matters (clockwise vs. counter-clockwise) because it determines which way the face is pointing (Normal).

        int vert = 0;

        //left side  
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        //vert++: This increments the index every time a point is added so the next point goes into the next slot in the array.

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        //right side  
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;

        for(int i = 0; i < segments; i++)
        {
            // Math to find the "current" and "next" slice of the curve
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;

            //far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            //top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            //bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;

            currentAngle += deltaAngle;
        }

        

        //the triangles array usually tells the engine: "Take vertex #5, #10, and #2 and make a triangle."
        //Since this script added vertices in the exact order it wanted them drawn, it simply maps index 0 to vertex 0, index 1 to vertex 1, and so on.
        for(int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        // Hands the list of points to the Mesh object.
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        //This is a vital helper function. It calculates which way each face is "looking" so that light bounces off it correctly. Without this, the mesh might appear black or invisible.
        mesh.RecalculateNormals();

        return mesh;
    }

    private void Scan()
    {
        playerCount = Physics.OverlapSphereNonAlloc(transform.position, distance, playerCollider, playerLayers, QueryTriggerInteraction.Collide);
        PlayerInSight = false;
        Player.Clear();
        for(int i = 0; i < playerCount; ++i)
        {
            GameObject obj = playerCollider[i].gameObject;
            if (InSight(obj))
            {
                Player.Add(obj);
                GetPlayerLocation();
                EnemyManager.Instance.ReportPlayerLocation(PlayersLocation);
                PlayerInSight = true;
            }
        }
    }

    public bool InSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        if(direction.y < 0 || direction.y > height){return false;}


        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > angle){return false;}

        origin.y += height / 2;
        dest.y = origin.y;
        if(Physics.Linecast(origin, dest, occlusionLayer)){return false;}

        return true;
    }

    private Vector3 GetPlayerLocation()
    {
        if (Player.Count > 0)
        {
            PlayersLocation = Player[0].transform.position;
        }

        return PlayersLocation;
    }

    private void EnemyScan()
    {
        enemyCount = Physics.OverlapSphereNonAlloc(transform.position, enemyDistance, enemyColliders,enemyLayers, QueryTriggerInteraction.Collide);

        Enemies.Clear();
        for(int i = 0; i < enemyCount; i++)
        {
            GameObject Eobj = enemyColliders[i].gameObject;

            Enemies.Add(Eobj);
            Enemies.Remove(this.gameObject);
            
            GetEnemiesLocations();
            
        }
        
    }

    private Vector3 GetEnemiesLocations()
    {
        foreach(GameObject Eobj in Enemies)
        {
            if(Eobj != null)
            {
                EnemyLocation = Eobj.transform.position;
            }
        }

        return EnemyLocation;
    }


    private bool PathObstructedCheck()
    {
        

        if(! agent.SamplePathPosition(3 << NavMesh.GetAreaFromName("G1"), maxDistance, out obstacleHit))
        {
            if(obstacleHit.mask == 0) // Area Mask 0 means an area that is not walkable/carved.
            {
                pathObstructed = true;
            }
            else
            {
                pathObstructed = false;
            }
        }


        return pathObstructed;
    }
    

    public GameObject GetClosestObject(List<GameObject> objects)
    {
        GameObject closestObject = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject obj in objects)
        {
            float dist = Vector3.Distance(obj.transform.position, currentPos);

            if(dist < minDist)
            {
                minDist = dist;
                closestObject = obj;
            }

        }
        return closestObject;
    }

    private void OnValidate()
    {
        aiFOVCone = CreateWedgeMesh();
        Scan();
    }

    private void OnDrawGizmos()
    {
        if (aiFOVCone && showAISensor)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(aiFOVCone, transform.position, transform.rotation);
        }

        if (showAISensor)
        {
            Gizmos.DrawWireSphere(transform.position, distance);

            Gizmos.color = Color.green;
            foreach (var obj in Player)
            {
                Gizmos.DrawSphere(obj.transform.position, 1f);
                Gizmos.DrawLine(this.transform.position, obj.transform.position);
            }   

            Gizmos.color = new Color(1, 0f, 0f, 0.65f);
            Gizmos.DrawWireSphere(transform.position, enemyDistance);  
            foreach(var Eobj in Enemies)
            {
                Gizmos.DrawSphere(Eobj.transform.position, 1.7f);
                Gizmos.DrawLine(this.transform.position, Eobj.transform.position);
            }      
        }


    }
}
