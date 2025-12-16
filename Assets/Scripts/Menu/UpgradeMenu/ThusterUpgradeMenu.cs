using UnityEngine;

public class ThusterUpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject upgradeMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ThusterUpgrade1()
    {
        Debug.Log("Selected Thruster Upgrade 1");
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
        this.gameObject.SetActive(false);
        upgradeMenu.SetActive(true);
        
    }
}
