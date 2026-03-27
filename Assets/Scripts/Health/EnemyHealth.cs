using System.Collections;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    [SerializeField] EnemySO enemySO;
    private float smoothVelocity = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        maxShield = enemySO.shield;
        maxHealth = enemySO.health;
    }



    protected override void ShieldRechargeCheck()
    {
        

        if (this.gameObject != null)
        {
            
            

            if(canRecharge && !gotShot)// fix: add a corutine later to add a delay for the isShot bool, maybe increase in update using += shield.
            {
                Debug.Log("Replace later");
                shield += smoothVelocity;
                UpdateShieldBar(shield, maxShield);     
            }
            
        }
    }
}
