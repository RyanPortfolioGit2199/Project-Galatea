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
        //characterStatsSO.shield = maxShield;
        //characterStatsSO.health = maxHealth;
    }
    public void OnParticleCollision(GameObject other)
    {
        ParticleInfo particleInfo = other.GetComponent<ParticleInfo>();
        if(particleInfo != null)
        {
            Debug.Log("Enemy says: Ouchie I took Shield Damage: "+ particleInfo.shieldDamage + "and health damage: " + particleInfo.healthDamage);
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
        UpdateHealthBar(characterStatsSO.health, maxHealth);
    }

    public void TakeShiedDamage(float GunDamage)
    {
        characterStatsSO.shield -= GunDamage;
        UpdateHealthBar(characterStatsSO.shield, maxHealth);
    }
}
