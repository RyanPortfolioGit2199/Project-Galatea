using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Slider ShieldBar;
    
    private float maxHealth;
    private float maxShield;
    [SerializeField] private CharacterStatsSO characterStatsSO;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        maxShield = characterStatsSO.shield;
        maxHealth = characterStatsSO.health;
    }
    public void OnParticleCollision(GameObject other)
    {
        Gun particleInfo = other.GetComponent<Gun>();
        if(particleInfo != null)
        {
            Debug.Log("Enemy says: Ouchie I took Shield Damage: "+ particleInfo.shieldDamage + "and health damage: " + particleInfo.healthDamage);

            TakeShieldDamage(particleInfo.shieldDamage);
            if(characterStatsSO.shield > 0) {return;}
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
        characterStatsSO.health -= GunDamage;
        characterStatsSO.health = Mathf.Max(characterStatsSO.health, 0f);
        UpdateHealthBar(characterStatsSO.health, maxHealth);
    }

    public void TakeShieldDamage(float GunDamage)
    {
        characterStatsSO.shield -= GunDamage;
        characterStatsSO.shield = Mathf.Max(characterStatsSO.shield, 0f);
        UpdateShieldBar(characterStatsSO.shield, maxHealth);
    }
}
