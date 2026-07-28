
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance {get; private set;}
    /* Moved the functionality from the UpgradeMenu script to an upgrade manager instance so I can swap upgrades 
    based on what the player purchased at the start of every scene/session.

    Might need to decouple the menu from the manager to avoid a whole bunch of to keep uneeded menus/assets being loaded
    in scenes where they  aren't needed (ie performance issues but size of this project not an issue but getting into the mind
    set is good.)
    */
    ActiveWeapon activeWeapon;
    ActiveShield activeShield;
    ActiveThruster activeThruster;

    const int WeaponUpgrade0 = 0;
    const int WeaponUpgrade1 = 1;
    const int WeaponUpgrade2 = 2;
    const int WeaponUpgrade3 = 3;

    const int ShieldUpgrade0 = 0;
    const int ShieldUpgrade1 = 1;
    const int ShieldUpgrade2 = 2;
    const int ShieldUpgrade3 = 3;

    const int ThrusterUpgrade0 = 0;
    const int ThrusterUpgrade1 = 1;
    const int ThrusterUpgrade2 = 2;
    const int ThrusterUpgrade3 = 3;

    


    [field: SerializeField] public int setPlayerWeapon{get; private set;}
    [field: SerializeField] public int setPlayerShield{get; private set;}
    [field: SerializeField] public int setPlayerThruster{get; private set;}
    
    [SerializeField] int playerDebugScene = 2;

    /*
        Change these lists later to get; private set to give access to other scripts
        to avoid excess bloat and might help with performance.(Not need for this size of project but good to get in the habit.)
    */
    [field: SerializeField] public List<UpgradesSO> weaponSOList {get; private set;}
    
    [field: SerializeField] public List<UpgradesSO> shieldSOList{get; private set;}

    [field: SerializeField] public List<UpgradesSO> thrusterSOList{get; private set;}
    
    
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        
        StartOfScene();
    }

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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Loaded scene: {scene.name}");
        // Put your scene-transition reset logic here
        StartOfScene();    
        //GainedCurrency(SaveManager.Instance.saveData.SavedCurrency);
    }
    private void StartOfScene()
    {
        activeWeapon = FindAnyObjectByType<ActiveWeapon>();
        activeShield = FindAnyObjectByType<ActiveShield>();
        activeThruster = FindAnyObjectByType<ActiveThruster>();
        SetSavedWeapon();
        SetSavedShields();
        SetSavedThruster();
    }

    public void UpgradeWeapon(int weaponUpgrade)
    {
        //activeWeapon.SwitchWeapon(weaponSO);

        switch (weaponUpgrade)
        {
            case WeaponUpgrade0:
            Debug.Log("Added Base Gun to Player");
            activeWeapon.SwitchWeapon(weaponSOList[0]);
            setPlayerWeapon = 0;
            break;

            case WeaponUpgrade1:
            Debug.Log("Added Weapon Upgrade 1 to Player");
            activeWeapon.SwitchWeapon(weaponSOList[1]);
            setPlayerWeapon = 1;
            break;

            case WeaponUpgrade2:
            Debug.Log("Added Weapon Upgrade 2 to Player");
            activeWeapon.SwitchWeapon(weaponSOList[2]);
            setPlayerWeapon = 2;
            break;

            case WeaponUpgrade3:
            Debug.Log("Added Weapon Upgrade 3 to Player");
            activeWeapon.SwitchWeapon(weaponSOList[3]);
            setPlayerWeapon = 3;
            break;
        }
    }

    public void UpgradeShield(int shieldUpgrade)
    {
        switch (shieldUpgrade)
        {
            case ShieldUpgrade0:
            Debug.Log("Added Base Shield to Player");
            
            activeShield.SwitchShield(shieldSOList[0]);
            setPlayerShield = 0;
            break;

            case ShieldUpgrade1:
            Debug.Log("Added Shield Upgrade 1 to Player");
            
            activeShield.SwitchShield(shieldSOList[1]);
            setPlayerShield = 1;
            break;

            case ShieldUpgrade2:
            Debug.Log("Added Shield Upgrade 2 to Player");
            setPlayerShield = 2;
            break;

            case ShieldUpgrade3:
            Debug.Log("Added Shield Upgrade 3 to Player");
            setPlayerShield = 3;
            
            break;
            
            default:
            break;
        }
    }

    public void UpgradeThruster(int thrusterUpgrade)
    {
        switch (thrusterUpgrade)
        {
            case ThrusterUpgrade0:
            Debug.Log("Added Base Thruster to Player");           
            activeThruster.SwitchThruster(thrusterSOList[0]);
            setPlayerThruster = 0;
            break;

            case ThrusterUpgrade1:
            Debug.Log("Added Thruster Upgrade 1 to Player");
            activeThruster.SwitchThruster(thrusterSOList[1]);
            setPlayerThruster = 1;
            break;

            case ThrusterUpgrade2:
            Debug.Log("Added Thruster Upgrade 2 to Player");
            setPlayerThruster = 2;
            break;

            case ThrusterUpgrade3:
            Debug.Log("Added Thruster Upgrade 3 to Player");
            setPlayerThruster = 3;
            break;
            
            default:
            break;
        }
    }

    public void ExitUpgradeLevel()
    {
        /* 
        Need to get the Level the Player previously completed and need to figure out how to 
        send the player to next level using the Player Debug Level as default currently.
        */

        SceneManager.LoadScene(playerDebugScene);

        /*
            {OLD}
            Add here:
            Save the player upgrades here

            {CURRENT}
            Moved to the saving function to the PurchaseManager PurchaseUIHandler script.
        */

        
    }


    public void SetSavedWeapon()
    {
        UpgradeWeapon(SaveManager.Instance.saveData.SavedPlayerWeapon);
    }

    public void SetSavedShields()
    {
        UpgradeShield(SaveManager.Instance.saveData.SavedPlayerShield);
    }

    public void SetSavedThruster()
    {
        UpgradeThruster(SaveManager.Instance.saveData.SavedPlayerThruster);
    }
    
}
