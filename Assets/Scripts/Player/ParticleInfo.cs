using UnityEngine;

public class ParticleInfo : MonoBehaviour
{   
    private ActiveWeapon activeWeapon;
    public float shieldDamage;
    public float healthDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeWeapon = GetComponentInParent<ActiveWeapon>();

        shieldDamage = activeWeapon.upgradesSO.ShieldDamage;
        healthDamage = activeWeapon.upgradesSO.Damage;
    }

    
}
