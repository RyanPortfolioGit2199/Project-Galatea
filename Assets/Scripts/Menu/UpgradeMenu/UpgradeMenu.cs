using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject gunMenu;
    [SerializeField] GameObject shieldMenu;
    [SerializeField] GameObject thrusterMenu;


    public void GunUpgrades()
    {
        this.gameObject.SetActive(false);
        gunMenu.SetActive(true);
    }

    public void ShieldUpgrades()
    {
        this.gameObject.SetActive(false);
        shieldMenu.SetActive(true);
    }

    public void ThrusterUpgrades()
    {
        this.gameObject.SetActive(false);
        thrusterMenu.SetActive(true);
    }
}
