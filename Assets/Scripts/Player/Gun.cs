using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    Scene currentScene;
    int UpgradeScene = 2; // Change if the UpgradeScene gets changed in the Scene List
    ParticleSystem bulletParticle;
    ActiveWeapon activeWeapon;

    void Awake()
    {
        bulletParticle = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        activeWeapon = GetComponentInParent<ActiveWeapon>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot (WeaponSO weaponSO)
    {
        if(currentScene.buildIndex == UpgradeScene) return;// makes it sure that the player cant shoot in the Upgrade menu.
        

        bulletParticle.Emit(1);
        
        
    }
}
