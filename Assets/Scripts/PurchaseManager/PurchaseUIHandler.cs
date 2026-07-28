using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PurchaseUIHandler : MonoBehaviour
{
    [SerializeField] Button buyButton;
    [SerializeField] Button equipButton;
    [SerializeField] GameObject buttonGroup;

    [SerializeField] int upgradeScene;

    Scene currentScene;
    

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Always unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This runs every single time a scene finishes loading
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartBehaviour();
    }

    void Start()
    {
        StartBehaviour();
    }

    void StartBehaviour()
    {
        currentScene = SceneManager.GetActiveScene();

        if(currentScene.buildIndex == upgradeScene)
        {
            buttonGroup.SetActive(true);
        }
        else
        {
            buttonGroup.SetActive(false);
        }
    }


    void Update()
    {
        if(PurchaseManager.Instance.canBuy == false)
        {
            buyButton.interactable = false;
        }
        else
        {
            buyButton.interactable = true;
        }

        if(PurchaseManager.Instance.isOwned == false)
        {
            equipButton.interactable = false;
        }
        else
        {
            equipButton.interactable = true;
        }
    }

    public void PurchaseUpgrades()
    {
        /*
            Move the function of Saving Upgrades from the UpgradeManager to here.

            Make the Button uninteractable when either they own the selected upgrade, havent selected any upgrades,
            or can't afford the select upgrade.
        */

        PurchaseManager.Instance.BuyingUpgrades();



        SaveManager.Instance.UpdateUpgrades(UpgradeManager.Instance.setPlayerWeapon, UpgradeManager.Instance.setPlayerShield, UpgradeManager.Instance.setPlayerThruster);
        Debug.Log("Saved Weapon" + SaveManager.Instance.saveData.SavedPlayerWeapon);
        Debug.Log("Saved Thruster" + SaveManager.Instance.saveData.SavedPlayerThruster);
        Debug.Log("Saved Shield" + SaveManager.Instance.saveData.SavedPlayerShield);
    }
    public void EquipButton()
    {
        /*
            Move the function of Saving Upgrades from the UpgradeManager to here.

            Make the Button uninteractable when either they haven't selected any upgrade, don't already own selected upgrade.
        */
        SaveManager.Instance.UpdateUpgrades(UpgradeManager.Instance.setPlayerWeapon, UpgradeManager.Instance.setPlayerShield, UpgradeManager.Instance.setPlayerThruster);

        Debug.Log(UpgradeManager.Instance.setPlayerWeapon);
    }

    
}
