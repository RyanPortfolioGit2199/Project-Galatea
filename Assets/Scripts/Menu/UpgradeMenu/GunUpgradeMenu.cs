using UnityEngine;
using TMPro;
public class GunUpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject upgradeMenu;
    [SerializeField] UpgradesSO[] weaponSO;
    [SerializeField] TextMeshProUGUI[] GunUpgradePrices;
    public PurchaseManager purchaseManager;

    void Awake()
    {
        

        GunUpgradePrices[0].SetText("$ " + weaponSO[1].Cost);
    }

    public void BaseGun()
    {
        Debug.Log("Base Gun");
        UpgradeManager.Instance.UpgradeWeapon(0);
        PurchaseManager.Instance.PurchasingCalculations(UpgradeManager.Instance.weaponSOList[0]);
    }

    public void GunUpgrade1()
    {
        Debug.Log("Gun Upgrade 1");
        UpgradeManager.Instance.UpgradeWeapon(1);
        PurchaseManager.Instance.PurchasingCalculations(UpgradeManager.Instance.weaponSOList[1]);

    }

    public void GunUpgrade2()
    {
        Debug.Log("Gun Upgrade 2");
        UpgradeManager.Instance.UpgradeWeapon(2);
        PurchaseManager.Instance.PurchasingCalculations(UpgradeManager.Instance.weaponSOList[2]);
    }

    public void GunUpgrade3()
    {
        Debug.Log("Gun Upgrade 3");
        UpgradeManager.Instance.UpgradeWeapon(3);
        PurchaseManager.Instance.PurchasingCalculations(UpgradeManager.Instance.weaponSOList[3]);
    }

    public void BackToUpgradeMenu()
    {

        UpgradeManager.Instance.UpgradeWeapon(SaveManager.Instance.saveData.SavedPlayerWeapon);
        PurchaseManager.Instance.GetCurrency();
        PurchaseManager.Instance.canBuy = false;
        this.gameObject.SetActive(false);
        upgradeMenu.SetActive(true);
                
        
    }


}
