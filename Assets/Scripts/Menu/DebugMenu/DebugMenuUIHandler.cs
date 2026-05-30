using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenuUIHandler : MonoBehaviour
{
    [Header("References")]
    
    [SerializeField] GameObject debuglevelMenu;
    [SerializeField] GameObject debugMenu;
    [SerializeField] GameObject debugUpgradeMenu;
    [SerializeField] GameObject debugSaveMenu;

    public void DebugLevelMenu()
    {
        debugMenu.SetActive(false);
        debuglevelMenu.SetActive(true);
    }

    public void DebugUpgradeMenu()
    {
        debugMenu.SetActive(false);
        debugUpgradeMenu.SetActive(true);
    }

    public void DebugSaveMenu()
    {
        debugMenu.SetActive(false);
        debugSaveMenu.SetActive(true);
    }

    public void ExitDebugMenu()
    {
        debugMenu.SetActive(false);
    }
}
