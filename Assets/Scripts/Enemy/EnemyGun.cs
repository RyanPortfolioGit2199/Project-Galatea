using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    ParticleSystem bulletParticle;
    public int shieldDamage;
    public int healthDamage;
    [SerializeField] WeaponSO enemyWeaponSO;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletParticle = GetComponent<ParticleSystem>();
        shieldDamage = enemyWeaponSO.ShieldDamage;
        healthDamage = enemyWeaponSO.Damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        Debug.Log("Enemy is firing Gun");
        bulletParticle.Emit(1);
    }
}
