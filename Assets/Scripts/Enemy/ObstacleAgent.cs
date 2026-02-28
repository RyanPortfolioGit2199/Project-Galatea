using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public class ObstacleAgent : MonoBehaviour
{
    [SerializeField] private float CarvingTime = 0.5f;
    [SerializeField] private float CarvingMoveThreshold = 0.1f;

    private NavMeshAgent Agent;
    private NavMeshObstacle Obstacle;

    

    private float LastMoveTime;
    private Vector3 LastPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Obstacle = GetComponent<NavMeshObstacle>();

        Obstacle.enabled = false;
        Obstacle.carveOnlyStationary = false;
        Obstacle.carving = true;

        LastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(LastPosition, transform.position) > CarvingMoveThreshold)
        {
            LastMoveTime = Time.time;
            LastPosition = transform.position;
        }
        if(LastMoveTime + CarvingTime < Time.time)
        {
            
            Agent.enabled = false;
            Obstacle.enabled = true;
        }
    }

    // Used for when having a set destination in mind
    public void SetDestination(Vector3 Position)
    {
        Obstacle.enabled = false;
        LastMoveTime = Time.time;
        LastPosition = transform.position;

        StartCoroutine(MoveAgent(Position));
    }

    private IEnumerator MoveAgent(Vector3 Position)
    {

        yield return null;
        Agent.enabled = true;

        
        Agent.SetDestination(Position);
    }

    public void SetRandomDestination(Vector3 Position)
    {
        Obstacle.enabled = false;
        LastMoveTime = Time.time;
        LastPosition = transform.position;

        StartCoroutine(RandomMoveAgent(Position));
    }

    private IEnumerator RandomMoveAgent(Vector3 Position)
    {
        yield return null;
        Agent.enabled = true;


        if (Agent.isActiveAndEnabled)
        {
            NavMeshHit navMeshHit;
            if(NavMesh.SamplePosition(Position, out navMeshHit, 4f, 3 << NavMesh.GetAreaFromName("G1")))// delete the 3 later and replace with custom area method value later.
            {
                Position = navMeshHit.position;
            } 

            Debug.Log(Position);

            Debug.DrawLine(Position, transform.position, Color.red, 5f);
            
        }

        Agent.SetDestination(Position);
    }
}
