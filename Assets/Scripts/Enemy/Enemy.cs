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
        Debug.Log(changeMind);

        if (playerIsNear)
        {
            //change to Chase State
            Debug.Log("Enemy: I can see the player, I am going to Chase you.");
        }
        else if(changeMind <= 0)
        {
            //change to Wander State
            Debug.Log("Enemy: The Player isn't in site. Going on Patrol.");
        }
    }

    void OnIdleExit()
    {
        
    }

    void OnPatrolEnter()
    {
        stateNote.text = "Patrol";
    }
}
