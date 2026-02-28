using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider HealthBar;
    [SerializeField] private float health;
    [SerializeField]private float maxHealth;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    public void UpdateHealthBar(float currentVale, float maxValue)
    {
        HealthBar.value = currentVale / maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float GunDamage)
    {
        health -= GunDamage;
        UpdateHealthBar(health, maxHealth);
    }
}
