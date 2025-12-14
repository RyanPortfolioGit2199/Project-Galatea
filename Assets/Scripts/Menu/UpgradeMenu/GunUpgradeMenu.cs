using UnityEngine;

public class GunUpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject upgradeMenu;
    public void GunUpgrade1()
    {
        Debug.Log("Gun Upgrade 1");
    }

    public void GunUpgrade2()
    {
        Debug.Log("Gun Upgrade 2");
    }

    public void GunUpgrade3()
    {
        Debug.Log("Gun Upgrade 3");
    }

    public void BackToUpgradeMenu()
    {
        this.gameObject.SetActive(false);
        upgradeMenu.SetActive(true);
        
    }
}
