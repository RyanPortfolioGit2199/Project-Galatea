using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PurchaseManager : MonoBehaviour
{
    public static PurchaseManager Instance {get; private set;}
    [SerializeField] UpgradeManager upgradeManager;

    const int upgradeType0 = 0;
    const int upgradeType1 = 1;
    const int upgradeType2 = 2;
    const int upgradeType3 = 3;

    [Header("Players Currency (DON'T EDIT THE VALUE IN THE INSPECTOR!!!!!!!!)")]
    public float playerCurrency;
    public float cartCurrency;
    public bool canBuy;
    public bool isOwned;
    public int whichUpgradeType;
    [SerializeField]ActiveWeapon activeWeapon;
    [SerializeField] ActiveShield activeShield;
    [SerializeField] ActiveThruster activeThruster;
    [SerializeField] UpgradesSO selectedUpgradesSO;

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

    // This runs every single time a scene finishes loading
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartBehaviour();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartBehaviour();
    }

    void StartBehaviour()
    {

        canBuy = false;
        isOwned = false;
        GetCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCurrency()
    {
        playerCurrency = CurrencyManager.Instance.currency;
        cartCurrency = playerCurrency;
    }

    public void PurchasingCalculations(UpgradesSO upgradesSO)
    {
        selectedUpgradesSO = upgradesSO;

        WhichUpgradeCheck(upgradesSO);

        if(isOwned){return;}

        GetCurrency();
        cartCurrency = playerCurrency - upgradesSO.Cost;

        CanPurchase(cartCurrency);
    }
    
    public void WhichUpgradeCheck(UpgradesSO upgradesSO)
    {
        switch (upgradesSO.upgradeType)
        {
            case UpgradeType.Weapon:
            Debug.Log("Checking which owned weapon");
            IsWeaponOwnedCheck(upgradesSO.UpgradeID);
            whichUpgradeType = 1;
            break;

            case UpgradeType.Thrusters:
            Debug.Log("Checking which owned Thruster");
            IsThrusterOwnedCheck(upgradesSO.UpgradeID);

            break;
            case UpgradeType.Shields:
            Debug.Log("Checking which owned Shield");
            IsShieldOwnedCheck(upgradesSO.UpgradeID);
            break;

            default:
            whichUpgradeType = 0;
            break;
        }

    }

    public bool IsWeaponOwnedCheck(int weaponID)
    {
        if (SaveManager.Instance.saveData.OwnedWeapons.Contains(weaponID))
        {
            isOwned = true;
        }
        else if (!SaveManager.Instance.saveData.OwnedWeapons.Contains(weaponID))
        {
            isOwned = false;
        }

        return isOwned;
    }

    public bool IsShieldOwnedCheck(int shieldID)
    {
        if (SaveManager.Instance.saveData.OwnedShields.Contains(shieldID))
        {
            isOwned = true;
        }
        else if (!SaveManager.Instance.saveData.OwnedShields.Contains(shieldID))
        {
            isOwned = false;
        }

        return isOwned;
    }

    public bool IsThrusterOwnedCheck(int thrusterID)
    {
        if (SaveManager.Instance.saveData.OwnedThrusters.Contains(thrusterID))
        {
            isOwned = true;
        }
        else if (!SaveManager.Instance.saveData.OwnedThrusters.Contains(thrusterID))
        {
            isOwned = false;
        }

        return isOwned;
    }

    public void WhichUpgradeToAdd(UpgradesSO upgradesSO)
    {
        switch (upgradesSO.upgradeType)
        {
            case UpgradeType.Weapon:
            SaveManager.Instance.UpdateOwnedWeapons(upgradesSO.UpgradeID);
            Debug.Log("Saving Weapon Upgrade "+upgradesSO.UpgradeID+" as owned");
            break;

            case UpgradeType.Thrusters:
            Debug.Log("Saving Thruster Upgrade "+upgradesSO.UpgradeID+" as owned");
            SaveManager.Instance.UpdateOwnedThrusters(upgradesSO.UpgradeID);
            break;

            case UpgradeType.Shields:
            Debug.Log("Saving Shield Upgrade "+ upgradesSO.UpgradeID +" as owned");
            SaveManager.Instance.UpdateOwnedShields(upgradesSO.UpgradeID);
            break;

            default:
            whichUpgradeType = 0;
            break;
        }
    }
// Make 2 other methods like this one for Shields and Thrusters

    public bool CanPurchase(float cart)
    {
        if(cart >= 0)
        {
            canBuy = true;
        }
        else if(cart < 0)
        {
            canBuy = false;
        }

        return canBuy;
    }


    public void BuyingUpgrades()
    {

        WhichUpgradeToAdd(selectedUpgradesSO);

        CurrencyManager.Instance.SpentCurrency(cartCurrency);
        playerCurrency = CurrencyManager.Instance.currency;
        canBuy = false;
        isOwned = false;
    }
}
