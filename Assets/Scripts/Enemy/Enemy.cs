
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public static Enemy Instance;

    [SerializeField] protected EnemySO enemySO;
    [SerializeField] protected TextMeshProUGUI stateNote;

    protected EnemyGun currentGun;
    
    protected StateMachine brain;
    protected NavMeshAgent agent;
    protected ObstacleAgent ObstacleAgent;
    protected NavMeshObstacle navObstacle;
    protected PlayerController player;
    [SerializeField] protected bool playerIsNear;
    [SerializeField] protected float Radius = 10f;
    [SerializeField] protected float offsetRadius = 30f;
    [SerializeField] bool drawPositionSphere;
    [SerializeField] protected float separationStrength = 20f;
    [SerializeField] protected float minStoppingDistance = 10f;
    [SerializeField] protected float maxStoppingDistance = 20f;
    protected bool withinAttackRange;
    protected float attackTimer;
    protected float changeMind;
    protected AISensor sensor;
    protected Vector3 wanderDistance;
    [SerializeField] protected float playerDistance;
    [SerializeField] protected float fleeDistance;

    
    protected bool destinationReached = true;

    protected Vector3 destination;


    protected virtual void Awake()
    {
        brain = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        sensor = GetComponent<AISensor>();
        ObstacleAgent = GetComponent<ObstacleAgent>();
        navObstacle = GetComponent<NavMeshObstacle>();
        currentGun = GetComponentInChildren<EnemyGun>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        

        playerIsNear = false;
        withinAttackRange = false;
        brain.PushState(Idle, OnIdleEnter, OnIdleExit);

        agent.avoidancePriority = Random.Range(10, 90);

        PlayerRange();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        

        withinAttackRange = Vector3.Distance(this.transform.position, EnemyManager.Instance.LastKnownPosition) < playerDistance;
    }

    protected virtual void OnDrawGizmos()
    {
        if (drawPositionSphere)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }

    protected virtual float PlayerRange()
    {
        playerDistance = Random.Range(enemySO.minDistanceFromPlayer, enemySO.maxDistanceFromPlayer);
        return playerDistance;
    }

    protected virtual void AimAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(EnemyManager.Instance.LastKnownPosition - transform.position);
        rotation.x = 0f;
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
    }

    protected virtual void ValidatePath(Vector3 Position)
    {
        if (destinationReached || agent.pathPending) return;

        // Check if the path is blocked by a carved hole (Partial)
        // or if the NavMesh update made the current path 'stale'
        if (agent.pathStatus == NavMeshPathStatus.PathPartial || agent.isPathStale)
        {
            // Re-request the path to the original destination. 
            // Because the obstacle has 'carved' a hole, the A* algorithm 
            // will now look for a way AROUND that hole to reach savedTarget.
            ObstacleAgent.SetDestination(Position);
            
            Debug.Log("Obstacle carved the mesh. Recalculating detour to: " + Position);
        }

        // Standard arrival check
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) 
        {
            destinationReached = true;
        }
    }

    protected virtual void ChaseStateStuckCheck()
    {
        
    }

    protected abstract void OnIdleEnter();
    

    protected abstract void Idle();
    

    protected abstract void OnIdleExit();
    

    protected abstract void OnChaseEnter();
    

    protected abstract void Chase();
    

    protected abstract void OnChaseExit();


    protected abstract void OnPatrolEnter();
    

    protected abstract void Patrol();
    

    protected abstract void OnPatrolExit();
    

    protected abstract void OnRepositionEnter();

    protected abstract void Reposition();
    

    protected abstract void OnRepositionExit();
    

    protected abstract void OnAttackEnter();
   

    protected abstract void Attack();
    
}
