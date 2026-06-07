using UnityEngine;
using UnityEngine.SceneManagement;
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
    const int WeaponUpgrade1 = 1;
    const int WeaponUpgrade2 = 2;
    const int WeaponUpgrade3 = 3;


    public int setPlayerWeapon{get; private set;}
    int setPlayerShield;
    int setPlayerThruster;
    
    [SerializeField] int playerDebugScene = 1;
    [SerializeField] WeaponSO[] weaponSO;
    [SerializeField] ShieldSO[] shieldSO;
    [SerializeField] ThrusterSO[] thrusterSO;
    
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
        SetSavedWeapon();
    }

    public void UpgradeWeapon(int weaponUpgrade)
    {
        //activeWeapon.SwitchWeapon(weaponSO);

        switch (weaponUpgrade)
        {
            case WeaponUpgrade1:
            Debug.Log("Added Weapon Upgrade 1 to Player");
            activeWeapon.SwitchWeapon(weaponSO[0]);
            setPlayerWeapon = 1;
            break;

            case WeaponUpgrade2:
            Debug.Log("Added Weapon Upgrade 2 to Player");
            break;

            case WeaponUpgrade3:
            Debug.Log("Added Weapon Upgrade 3 to Player");
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
            Add here:
            Save the player upgrades here
        */

        SaveManager.Instance.UpdateUpgrades(setPlayerWeapon, setPlayerShield, setPlayerThruster);
    }


    public void SetSavedWeapon()
    {
        UpgradeWeapon(SaveManager.Instance.saveData.SavedPlayerWeapon);
    }
}
