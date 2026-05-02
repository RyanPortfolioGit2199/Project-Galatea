using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthSystem : MonoBehaviour
{
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Slider ShieldBar;
    [SerializeField] bool isBrute;
    
    public float health;
    public float shield;
    public float maxHealth;
    public float maxShield;
    public bool gotShot;
    public Coroutine timerRoutine;

    public bool canRecharge;
    private float smoothVelocity = 0.1f;
    
    


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
        Debug.Log(shield);
        shield -= GunDamage;
        Debug.Log(shield + "After Damage");
        shield = Mathf.Max(shield, 0f); // somehow player is calculating x5 damage when taken need to look into it
        UpdateShieldBar(shield, maxShield);
    }

    //var name = "Scott"; var count = 3; var msg = $"Hello {name}, you have {count} items.";

    public void ShieldRechargeCheck()
    {
        if (this.gameObject != null)
        {
            
            if(this.shield == maxShield) {return;}

            if(canRecharge && !gotShot)// fix: add a corutine later to add a delay for the isShot bool, maybe increase in update using += shield.
            {
                Debug.Log("Replace later");
                this.shield += smoothVelocity;
                UpdateShieldBar(shield, maxShield);     
            }
            
        }
    }

    protected abstract void CanRechargeCheck();
    protected abstract void Die();
        // Refactor later to implement either/both a dieing animation, a particle explosion. Before

    public IEnumerator ShotTimer()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Setting gotShot to false because " + this.name + " hasn't been shot in the past 3 seconds");
        gotShot = false;
    }

    
    
}
