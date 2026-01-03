using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    Scene currentScene;
    int UpgradeScene = 2; // Change if the UpgradeScene gets changed in the Scene List
    [SerializeField] GameObject gunBullets;
    ActiveWeapon activeWeapon;

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
        
        
        var emissionModule = gunBullets.GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = activeWeapon.isFiring;
    }
}
