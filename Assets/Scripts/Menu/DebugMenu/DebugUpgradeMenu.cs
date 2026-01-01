using UnityEngine;

public class DebugUpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject debugMenu;
    [SerializeField] GameObject debugUpgrade;

    [SerializeField] WeaponSO[] weaponSO;
    ActiveWeapon activeWeapon;

    void Awake()
    {
        activeWeapon = FindAnyObjectByType<ActiveWeapon>();
    }
    public void GunUpgrade1()
    {
        Debug.Log("Gun Upgrade 1");
        activeWeapon.SwitchWeapon(weaponSO[0]);
    }

    public void GunUpgrade2()
    {
        Debug.Log("Gun Upgrade 2");
    }

    public void GunUpgrade3()
    {
        Debug.Log("Gun Upgrade 3");
    }

    public void BackToMenu()
    {
        debugUpgrade.SetActive(false);
        debugMenu.SetActive(true);
    }
}
