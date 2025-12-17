using UnityEngine;

public class ThusterUpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject upgradeMenu;

    [SerializeField] ThrusterSO[] thrusterSO;

    ActiveThruster activeThruster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        activeThruster = FindAnyObjectByType<ActiveThruster>();
    }
    public void ThusterUpgrade1()
    {
        Debug.Log("Selected Thruster Upgrade 1");
       activeThruster.SwitchThruster(thrusterSO[0]);
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
