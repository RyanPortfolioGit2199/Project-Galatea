using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject gunMenu;


    public void GunUpgrades()
    {
        gunMenu.SetActive(true);
        this.gameObject.SetActive(false);
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
