using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject gunMenu;


    public void GunUpgrades()
    {
        this.gameObject.SetActive(false);
        gunMenu.SetActive(true);
    }

    public void ShieldUpgrades()
    {
        Debug.Log("Open Shield Upgrade Menu");
    }

    public void ThrusterUpgrades()
    {
        Debug.Log("Open Thruster Upgrade Menu");
    }
}
