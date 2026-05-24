using UnityEngine;
using System;

public class LevelOneObjective : ObjectiveScripte
{
    
    private int enemiesKilled = 0;
    private int enemiesToKill = 5;

    void OnEnable()
    {
        EnemyHealth.onEnemyDeath += EnemiesKilled;
        
        
    }

    private void OnDisable()
    {
        EnemyHealth.onEnemyDeath -= EnemiesKilled;

    }

    void EnemiesKilled()
    {
        if(enemiesKilled < enemiesToKill)
        {
            enemiesKilled++;
            Debug.Log(name +"EnemiesKilled: " + enemiesKilled);
        }

        if (enemiesKilled >= enemiesToKill)
        {
            FinishObjectiveStep();
        }
    }
}
