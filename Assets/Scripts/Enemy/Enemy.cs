
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemySO enemySO;
    [SerializeField] TextMeshProUGUI stateNote;
    [SerializeField] EnemyType currentEnemyType;
    private StateMachine brain;
    private NavMeshAgent agent;
    private PlayerController player;
    [SerializeField] bool playerIsNear;
    [SerializeField] float Radius = 10f;
    private bool withinAttackRange;
    private float attackTimer;
    private float changeMind;

    private Vector3 destination;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        brain = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        player = FindAnyObjectByType<PlayerController>();

        playerIsNear = false;
        withinAttackRange = false;
        brain.PushState(Idle, OnIdleEnter, OnIdleExit);

        
    }

    // Update is called once per frame
    void Update()
    {
        playerIsNear = Vector3.Distance(this.transform.position, player.transform.position) < enemySO.distanceFromPlayer;
        withinAttackRange = Vector3.Distance(this.transform.position, player.transform.position) < enemySO.attackRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    public enum EnemyType
    {
        Grunt, 
        Brute
    };

    private void AimAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
    }


    void OnIdleEnter()
    {
        agent.ResetPath();
        stateNote.text = "Idle";
    }

    void Idle()
    {
        changeMind -= Time.deltaTime;
        //Debug.Log(changeMind);

        if (playerIsNear)
        {
            brain.PushState(Chase, OnChaseEnter, OnChaseExit);
            Debug.Log("Enemy: I can see the player, I am going to Chase you.");
        }
        if(changeMind <= 0)
        {
            brain.PushState(Patrol, OnPatrolEnter, OnPatrolExit);
            Debug.Log("Enemy: The Player isn't in site. Going on Patrol.");
            changeMind = Random.Range(4, 10);
        }
    }

    void OnIdleExit()
    {
        
    }

    void OnChaseEnter()
    {
        stateNote.text = "Chase";
    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);
        
        if(Vector3.Distance(this.transform.position, player.transform.position) > 45.5f)
        {
            brain.PushState(Idle, OnIdleEnter, OnIdleExit);
        }
        
        if (withinAttackRange)
        {
            brain.PushState(Attack, OnAttackEnter, null);
        }
        
    }

    void OnChaseExit()
    {
        
    }

    void OnPatrolEnter()
    {
        stateNote.text = "Patrol";

        Vector3 wanderDistance = (Random.insideUnitSphere * Radius) + transform.position;
        wanderDistance.y = 0f;
        Debug.Log(wanderDistance);

        NavMeshHit navMeshHit;
        

        

        if(NavMesh.SamplePosition(wanderDistance, out navMeshHit, 4f, 3 << NavMesh.GetAreaFromName("G1")))// delete the 3 later and replace with custom area method value later.

        {
            destination = navMeshHit.position;
        } 
        

        Debug.Log(destination);

        Debug.DrawLine(destination, transform.position, Color.red, 5f);


        agent.SetDestination(destination);
    }

    void Patrol()
    {
        if(agent.remainingDistance <= .25f)
        {
            agent.ResetPath();
            brain.PushState(Idle, OnIdleEnter, OnIdleExit);
        }
        if (playerIsNear)
        {
            brain.PushState(Chase, OnChaseEnter, OnChaseExit);
        }
    }

    void OnPatrolExit()
    {
        
    }

    void OnAttackEnter()
    {
        agent.ResetPath();
        stateNote.text = "Attack";
    }

    void Attack()
    {
        attackTimer -= Time.deltaTime;
        AimAtPlayer();
        if (!withinAttackRange)
        {
            brain.PopState();
        }
        else if (attackTimer <= 0)
        {
            Debug.Log("Enemy: Pew Pew Pew firing my gun at the Player!!!");
            attackTimer = enemySO.fireRate;
        }
    }





    
}
