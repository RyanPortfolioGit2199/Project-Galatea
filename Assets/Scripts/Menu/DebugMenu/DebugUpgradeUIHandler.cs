using UnityEngine;

public class DebugUpgradeUIHandler : MonoBehaviour
{
    [SerializeField] GameObject debugMenu;
    [SerializeField] GameObject debugUpgradeMenu;

    public void DebugUpgradeMenu()
    {
        debugUpgradeMenu.SetActive(true);
        debugMenu.SetActive(false);
    }

    
}
