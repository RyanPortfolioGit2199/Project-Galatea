using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gun : MonoBehaviour
{
    Scene currentScene;
    int UpgradeScene = 3; // Change if the UpgradeScene gets changed in the Scene List
    ParticleSystem bulletParticle;
    ActiveWeapon activeWeapon;
    public float shieldDamage;
    public float healthDamage;

    void Awake()
    {
        bulletParticle = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        activeWeapon = GetComponentInParent<ActiveWeapon>();

        

        shieldDamage = activeWeapon.upgradesSO.ShieldDamage;
        healthDamage = activeWeapon.upgradesSO.Damage;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot (UpgradesSO weaponSO)
    {
        if(currentScene.buildIndex == UpgradeScene) return;// makes it sure that the player cant shoot in the Upgrade menu.


        if (weaponSO.isBurst)
        {
            StartCoroutine(BurstRoutine(3));
        }
        else if (!weaponSO.isBurst)
        {
            bulletParticle.Play();
        }
        
        
    }


    private IEnumerator BurstRoutine(int bulletAmount)
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            bulletParticle.Play();
            // Adjust the wait time between shots for your desired burst rate (e.g., 0.1 seconds)
            yield return new WaitForSeconds(0.1f);
        }
    }
}
