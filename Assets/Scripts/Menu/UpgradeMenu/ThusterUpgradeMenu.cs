using UnityEngine;

public class ThusterUpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject upgradeMenu;

    [SerializeField] UpgradesSO[] thrusterSO;

    ActiveThruster activeThruster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        activeThruster = FindAnyObjectByType<ActiveThruster>();
    }

    public void BaseThruster()
    {
        Debug.Log("Selected Base Thruster");
        UpgradeManager.Instance.UpgradeThruster(0);
        PurchaseManager.Instance.PurchasingCalculations(UpgradeManager.Instance.thrusterSOList[0]);
    }

    public void ThusterUpgrade1()
    {
        Debug.Log("Selected Thruster Upgrade 1");
        UpgradeManager.Instance.UpgradeThruster(1);
        PurchaseManager.Instance.PurchasingCalculations(UpgradeManager.Instance.thrusterSOList[1]);
    }

    public void ThusterUpgrade2()
    {
        Debug.Log("Selected Thruster Upgrade 2");
    }

    public void ThusterUpgrade3()
    {
        Debug.Log("Selected Thruster Upgrade 3");
    }

    public void BackToUpgradeMenu()
    {
        UpgradeManager.Instance.UpgradeWeapon(SaveManager.Instance.saveData.SavedPlayerThruster);
        PurchaseManager.Instance.GetCurrency();
        PurchaseManager.Instance.canBuy = false;
        this.gameObject.SetActive(false);
        upgradeMenu.SetActive(true);
        
    }
}
