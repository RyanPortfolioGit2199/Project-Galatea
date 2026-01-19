
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemySO enemySO;
    [SerializeField] protected TextMeshProUGUI stateNote;
    
    protected StateMachine brain;
    protected NavMeshAgent agent;
    protected PlayerController player;
    [SerializeField] protected bool playerIsNear;
    [SerializeField] protected float Radius = 10f;
    protected bool withinAttackRange;
    protected float attackTimer;
    protected float changeMind;

    protected Vector3 destination;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        brain = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        player = FindAnyObjectByType<PlayerController>();

        playerIsNear = false;
        withinAttackRange = false;
        brain.PushState(Idle, OnIdleEnter, OnIdleExit);

        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        playerIsNear = Vector3.Distance(this.transform.position, player.transform.position) < enemySO.distanceFromPlayer;
        withinAttackRange = Vector3.Distance(this.transform.position, player.transform.position) < enemySO.attackRange;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    

    protected virtual void AimAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
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
