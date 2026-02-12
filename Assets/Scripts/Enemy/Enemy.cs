
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public static Enemy Instance;

    [SerializeField] protected EnemySO enemySO;
    [SerializeField] protected TextMeshProUGUI stateNote;
    
    protected StateMachine brain;
    protected NavMeshAgent agent;
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
    [SerializeField] protected float playerDistance;

    

    protected Vector3 destination;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        brain = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        sensor = GetComponent<AISensor>();
        

        playerIsNear = false;
        withinAttackRange = false;
        brain.PushState(Idle, OnIdleEnter, OnIdleExit);

        PlayerRange();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Vector3 separationForce = Avoidance();
        Vector3 combinedVelocity = agent.desiredVelocity + (separationForce * separationStrength);
        agent.velocity = combinedVelocity;

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
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
    }

    protected virtual Vector3 Avoidance()
    {
        Vector3 force = Vector3.zero;

        if(sensor.Enemies.Count > 0)
        {
            foreach(var obj in sensor.Enemies)
            {
                if(obj.gameObject != gameObject)
                {
                    Vector3 diff = transform.position - obj.transform.position;

                    force += diff.normalized / Mathf.Max(diff.magnitude, 0.5f);
                }
            }
        }
        return force;
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
