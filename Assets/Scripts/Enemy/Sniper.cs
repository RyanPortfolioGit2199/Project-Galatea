using UnityEngine;

public class Sniper : Enemy
{
protected override void OnIdleEnter()
    {

        
        stateNote.text = "Idle";
    }

    protected override void Idle()
    {
        changeMind -= Time.deltaTime;
        //Debug.Log(changeMind);

        if (EnemyManager.Instance.PlayerDetected)
        {
            brain.PushState(Chase, OnChaseEnter, OnChaseExit);
            //Debug.Log("Enemy: I can see the player, I am going to Chase you.");
        }
        if(changeMind <= 0)
        {
            brain.PushState(Patrol, OnPatrolEnter, OnPatrolExit);
            
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

        agent.stoppingDistance = playerDistance;

        
        
        ObstacleAgent.SetDestination(EnemyManager.Instance.LastKnownPosition);
        //destinationReached = false;
           

        //Avoidance();

        if (!EnemyManager.Instance.PlayerDetected)
        {
            brain.PushState(Patrol, OnPatrolEnter, OnPatrolExit);
        }

        if (withinAttackRange)
        {
            brain.PushState(Reposition, OnRepositionEnter, OnRepositionExit);
        }

        
    }

    protected override void OnChaseExit()
    {
        
    }

    protected override void OnPatrolEnter()
    {
        

        stateNote.text = "Patrol";

        wanderDistance = (Random.insideUnitSphere * Radius) + transform.position;
        wanderDistance.y = 0f;
        //Debug.Log(wanderDistance);

        ObstacleAgent.SetRandomDestination(wanderDistance);
        
        
    }

    protected override void Patrol()
    {

        

        

        if(navObstacle.enabled == true)
        {
            
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
        
        PlayerRange();
        
        stateNote.text = "Reposition";
        agent.updateRotation = false;
        Vector3 newDest = EnemyManager.Instance.RepositionLocation();
        newDest.y = 0;

        ObstacleAgent.SetDestination(newDest);
        
    }

    protected override void Reposition()
    {
        agent.stoppingDistance = playerDistance;
        
        AimAtPlayer();

        if (navObstacle.enabled && !sensor.PlayerInSight)
        {
            brain.PushState(Reposition, OnRepositionEnter, OnRepositionExit);
        }   

        if (!withinAttackRange)
        {
            brain.PushState(Chase, OnChaseEnter, OnChaseExit);
        }

        if (sensor.PlayerInSight && navObstacle.enabled)
        {
            brain.PushState(Attack, OnAttackEnter, null);
        }

    }

    protected override void OnRepositionExit()
    {
        
    }

    protected override void OnAttackEnter()
    {
        if (agent.enabled)
        {
            agent.ResetPath();
        }
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
        else if (!sensor.PlayerInSight)
        {
            Debug.Log(name + " says: Player isn't in my line of sight");
            brain.PushState(Reposition, OnRepositionEnter, OnRepositionExit);
        }
        else if (attackTimer <= 0)
        {
            Debug.Log("Enemy: Pew Pew Pew firing my gun at the Player!!!");
            attackTimer = enemySO.fireRate;
        }
    }
}
