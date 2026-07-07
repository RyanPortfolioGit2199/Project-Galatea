using UnityEngine;

public class ShieldUpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject upgradeMenu;

    [SerializeField] UpgradesSO[] shieldSO;

    ActiveShield activeShield;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        activeShield = FindAnyObjectByType<ActiveShield>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BaseUpgrade()
    {
        Debug.Log("Selected Base Shield");
        UpgradeManager.Instance.UpgradeShield(0);
        PurchaseManager.Instance.PurchasingCalculations(shieldSO[0]);
    }

    public void ShieldUpgrade1()
    {
        Debug.Log("Selected Shield Upgrade 1");
        UpgradeManager.Instance.UpgradeShield(1);
        PurchaseManager.Instance.PurchasingCalculations(shieldSO[1]);
    }

    public void ShieldUpgrade2()
    {
        Debug.Log("Selected Shield Upgrade 2");
    }

    public void ShieldUpgrade3()
    {
        Debug.Log("Selected Shield Upgrade 3");
    }

    public void BackToUpgradeMenu()
    {
        UpgradeManager.Instance.UpgradeWeapon(SaveManager.Instance.saveData.SavedPlayerShield);
        PurchaseManager.Instance.GetCurrency();
        PurchaseManager.Instance.canBuy = false;
        this.gameObject.SetActive(false);
        upgradeMenu.SetActive(true);
        
    }
}
