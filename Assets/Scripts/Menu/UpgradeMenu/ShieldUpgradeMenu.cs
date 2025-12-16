using UnityEngine;

public class ShieldUpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject upgradeMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShieldUpgrade1()
    {
        Debug.Log("Selected Shield Upgrade 1");
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
        this.gameObject.SetActive(false);
        upgradeMenu.SetActive(true);
        
    }
}
