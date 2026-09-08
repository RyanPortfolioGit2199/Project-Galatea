using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugUpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject debugMenu;
    [SerializeField] GameObject debugUpgrade;

    [SerializeField] WeaponSO[] weaponSO;
    ActiveWeapon activeWeapon;

    void Awake()
    {
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
        
    }

    // Gun Upgrades

    public void GunUpgrade1()
    {
        Debug.Log("Gun Upgrade 1");
        activeWeapon.SwitchWeapon(UpgradeManager.Instance.weaponSOList[1]);
    }

    public void GunUpgrade2()
    {
        Debug.Log("Gun Upgrade 2");
    }

    public void GunUpgrade3()
    {
        Debug.Log("Gun Upgrade 3");
    }

    // ------------------------------------------------------------------------------------------------

    public void BackToMenu()
    {
        debugUpgrade.SetActive(false);
        debugMenu.SetActive(true);
    }


    
    public void DebugAddCurrency()
    {
        CurrencyManager.Instance.GainedCurrency(100);
    }

}
