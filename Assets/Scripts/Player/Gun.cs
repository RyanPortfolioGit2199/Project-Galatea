using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    Scene currentScene;
    int UpgradeScene = 2; // Change if the UpgradeScene gets changed in the Scene List
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot (WeaponSO weaponSO)
    {
        if(currentScene.buildIndex == UpgradeScene) return;// makes it sure that the player cant shoot in the Upgrade menu.
        RaycastHit gunHit;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out gunHit, Mathf.Infinity))
        {
            Debug.Log(gunHit.collider.name);
            Debug.DrawRay(this.transform.position, this.transform.forward);
        }
    }
}
