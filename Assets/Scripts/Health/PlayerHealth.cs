using UnityEngine;
using System.Collections;

public class PlayerHealth : HealthSystem
{
    [SerializeField] ActiveShield activeShield;
    
    [SerializeField]CharacterStatsSO PlayerHealthsSO;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        maxShield = activeShield.shieldSO.ShieldAmount;

        maxHealth = PlayerHealthsSO.health;

        Debug.Log("Player MaxHealth" + maxHealth + "and MaxShield" +maxShield);
    }

    // Update is called once per frame
    
    public void OnParticleCollision(GameObject other)
    {
        // look into the player shooting themselves
        try
        {
            EnemyGun particleInfo = other.GetComponent<EnemyGun>();
        Debug.Log("Player says: Ouchie I took Shield Damage: "+ particleInfo.shieldDamage + "and health damage: " + particleInfo.healthDamage);
        if(particleInfo != null)
        {
            gotShot = true;
            canRecharge = false;
            
            StartCoroutine(ShotTimer());
            TakeShieldDamage(particleInfo.shieldDamage);
            Debug.Log("Player is at " + health + "health and " + shield + "shields");
            
            if(shield <= 0) {TakeHealthDamage(particleInfo.healthDamage);}
            
            
        }
        
        }
        catch (System.Exception)
        {
            
            Debug.Log("I shot myself");
        }
    }   

    protected override void Die()
    {
        if (health <= 0)
        {
            Debug.Log(name + "says: I am Dead");
            GameManager.Instance.TriggerGameOver();
            this.gameObject.SetActive(false);
        }
    }
    
    public IEnumerator RechargeTimer()
    {
        yield return new WaitForSeconds(activeShield.shieldSO.RechargeRate);
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
}
