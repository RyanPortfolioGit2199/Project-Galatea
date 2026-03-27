using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthSystem : MonoBehaviour
{
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Slider ShieldBar;
    [SerializeField] bool isBrute;
    
    private float health;
    public float shield;
    public float maxHealth;
    public float maxShield;
    public bool gotShot;
    public Coroutine timerRoutine;

    public bool canRecharge;
    
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        shield = maxShield;
    }


    void Update()
    {
        if(shield < maxShield && !isBrute)
        {
            CanRechargeCheck();
            ShieldRechargeCheck();
        }
        
        Die();  
    }

    private void Die()
    {
        // Refactor later to implement either/both a dieing animation, a particle explosion. Before 

        if (health <= 0)
        {
            Debug.Log(name + "says: I am Dead");
            Destroy(this.gameObject);
        }
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
    public void UpdateHealthBar(float currentVale, float maxValue)
    {
        HealthBar.value = currentVale / maxValue;
    }
    public void UpdateShieldBar(float currentVale, float maxValue)
    {
        ShieldBar.value = currentVale / maxValue;
    }

    public void TakeHealthDamage(float GunDamage)
    {
        health -= GunDamage;
        health = Mathf.Max(health, 0f);
        UpdateHealthBar(health, maxHealth);
    }

    public void TakeShieldDamage(float GunDamage)
    {
        shield -= GunDamage;
        shield = Mathf.Max(shield, 0f);
        UpdateShieldBar(shield, maxShield);
    }

    protected abstract void ShieldRechargeCheck();

    public void CanRechargeCheck()
    {
        

        if (this.gameObject != null)
        {
            if(!gotShot && timerRoutine == null)
            {
                StartCoroutine(RechargeTimer());
            }
            else if (gotShot)
            {
                Debug.Log("Got Shot canceled recharge");
                canRecharge = false;
                StopCoroutine(RechargeTimer());
                
                
            }
        }
        
    }

    IEnumerator ShotTimer()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Setting gotShot to false because " + this.name + " hasn't been shot in the past 3 seconds");
        gotShot = false;
    }

    IEnumerator RechargeTimer()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Shield executed after 6 seconds of"+this.name + " being false");
        timerRoutine = null;
        canRecharge = true;
    }
    
}
