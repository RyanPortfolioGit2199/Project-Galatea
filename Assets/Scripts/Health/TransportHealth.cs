using System.Collections;
using UnityEngine;

public class TransportHealth : HealthSystem
{
    [SerializeField]CharacterStatsSO TransportStatsSO;

    void Awake()
    {
        maxHealth = TransportStatsSO.health;
        maxShield = TransportStatsSO.shield;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    


    public void OnParticleCollision(GameObject other)
    {
        // look into the player shooting themselves
            EnemyGun particleInfo = other.GetComponent<EnemyGun>();
        Debug.Log("Transport Ship says: Ouchie I took Shield Damage: "+ particleInfo.shieldDamage + "and health damage: " + particleInfo.healthDamage);
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


    public IEnumerator RechargeTimer()
    {
        yield return new WaitForSeconds(5f);
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
            Debug.Log(name + "says: I am Dead");
            GameManager.Instance.TriggerGameOver();
            this.gameObject.SetActive(false);
        }
    }

}
