using System.Collections;
using UnityEngine;
using System;

public class EnemyHealth : HealthSystem
{
    [SerializeField] EnemySO enemySO;
    
    
    public static event Action<EnemyHealth> OnEnemyDeath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        maxShield = enemySO.shield;
        maxHealth = enemySO.health;
    }

    public void OnParticleCollision(GameObject other)
    {
        Gun particleInfo = other.GetComponent<Gun>();
        if(particleInfo != null)
        {
            gotShot = true;
            canRecharge = false;
            Debug.Log("Enemy says: Ouchie I took Shield Damage: "+ particleInfo.shieldDamage + "and health damage: " + particleInfo.healthDamage);
            StartCoroutine(ShotTimer());
            TakeShieldDamage(particleInfo.shieldDamage);
            
            if(shield <= 0) {TakeHealthDamage(particleInfo.healthDamage);}
            
            
        }
        
    }

    public IEnumerator RechargeTimer()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Shield executed after 6 seconds of"+this.name + " being false");
        timerRoutine = null;
        canRecharge = true;
    }

    protected override void CanRechargeCheck()
    {
         if (this.gameObject != null)
        {
            if(!gotShot && timerRoutine == null)
            {
                timerRoutine = StartCoroutine(RechargeTimer());
            }
            else if (gotShot)
            {
                Debug.Log("Got Shot canceled recharge");
                canRecharge = false;
                StopCoroutine(RechargeTimer());
                
                
            }
        }
    }

    protected override void Die()
    {
        if (health <= 0)
        {
            OnEnemyDeath?.Invoke(this);
            Debug.Log(name + "says: I am Dead");
            Destroy(this.gameObject);
        }
    }

   
    
}
