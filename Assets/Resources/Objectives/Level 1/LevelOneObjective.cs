using UnityEngine;
using System;

public class LevelOneObjective : ObjectiveScripte
{
    public HealthSystem unitToWatch;
    private int enemiesKilled = 0;
    private int enemiesToKill = 5;

    void OnEnable()
    {
        EnemyHealth.OnEnemyDeath += EnemiesKilled;
        
        
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= EnemiesKilled;

    }

    void EnemiesKilled(EnemyHealth data)
    {
        if(enemiesKilled < enemiesToKill)
        {
            enemiesKilled++;
            Debug.Log("Enemy died: " + data.name + "EnemiesKilled: " + enemiesKilled);
        }

        if (enemiesKilled >= enemiesToKill)
        {
            FinishObjectiveStep();
        }
    }
}
