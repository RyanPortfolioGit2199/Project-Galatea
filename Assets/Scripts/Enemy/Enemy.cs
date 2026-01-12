using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemySO enemySO;
    [SerializeField] TextMeshProUGUI stateNote;
    private StateMachine brain;
    private NavMeshAgent agent;
    private PlayerController player;
    private bool playerIsNear;
    private bool withinAttackRange;
    private float attackTimer;
    private float changeMind;

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
        if(Vector3.Distance(this.transform.position, player.transform.position) > 5.5f)
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

        Vector3 wanderDistance = (Random.insideUnitSphere * 4f) + transform.position;
        NavMeshHit navMeshHit;


        NavMesh.SamplePosition(wanderDistance, out navMeshHit, 1f, NavMesh.AllAreas);

        Vector3 destination = navMeshHit.position;

        Debug.Log(destination);

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
