using UnityEngine;

public class EnemyHealth : HealthSystem
{
    [SerializeField] EnemySO enemySO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        maxShield = enemySO.shield;
        maxHealth = enemySO.health;
    }



    protected override void ShieldRecharge()
    {
        Debug.Log("Replace later");
    }
}
