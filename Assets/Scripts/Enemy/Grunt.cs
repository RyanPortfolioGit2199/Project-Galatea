using UnityEngine;
using UnityEngine.AI;
public class Grunt : Enemy
{
    protected override void OnIdleEnter()
    {
        agent.ResetPath();
        stateNote.text = "Idle";
    }

    protected override void Idle()
    {
        changeMind -= Time.deltaTime;
        //Debug.Log(changeMind);

        if (EnemyManager.Instance.PlayerDetected)
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

    protected override void OnIdleExit()
    {
        
    }

    protected override void OnChaseEnter()
    {
        stateNote.text = "Chase";
        PlayerRange();
        Debug.Log(name + "distance from player is" + playerDistance);
    }

    protected override void Chase()
    {
        Vector3 offset = Random.insideUnitSphere * offsetRadius;
        offset.y = 0f;

        //agent.stoppingDistance = Random.Range(minStoppingDistance, maxStoppingDistance);

        agent.SetDestination(EnemyManager.Instance.LastKnownPosition + offset);
        
        
        
        if (withinAttackRange)
        {
            brain.PushState(Attack, OnAttackEnter, null);
        }
    }

    protected override void OnChaseExit()
    {
        
    }

    protected override void OnPatrolEnter()
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

    protected override void Patrol()
    {
        if(agent.remainingDistance <= .25f)
        {
            agent.ResetPath();
            brain.PushState(Idle, OnIdleEnter, OnIdleExit);
        }
        if (EnemyManager.Instance.PlayerDetected)
        {
            brain.PushState(Chase, OnChaseEnter, OnChaseExit);
        }
    }

    protected override void OnPatrolExit()
    {
        
    }

    protected override void OnRepositionEnter()
    {
        
    }

    protected override void Reposition()
    {
        
    }

    protected override void OnRepositionExit()
    {
        
    }

    protected override void OnAttackEnter()
    {
        agent.ResetPath();
        stateNote.text = "Attack";
    }

    protected override void Attack()
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
