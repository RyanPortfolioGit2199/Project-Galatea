using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Slider ShieldBar;
    
    private float health;
    private float shield;
    private float maxHealth;
    private float maxShield;
    [SerializeField] private CharacterStatsSO characterStatsSO;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        maxShield = characterStatsSO.shield;
        maxHealth = characterStatsSO.health;
        health = maxHealth;
        shield = maxShield;
    }


    void Update()
    {
        if(health <= 0)
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
            Debug.Log("Enemy says: Ouchie I took Shield Damage: "+ particleInfo.shieldDamage + "and health damage: " + particleInfo.healthDamage);

            TakeShieldDamage(particleInfo.shieldDamage);
            if(shield > 0) {return;}
            TakeHealthDamage(particleInfo.healthDamage);
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
}
