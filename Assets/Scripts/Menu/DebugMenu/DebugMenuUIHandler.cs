using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenuUIHandler : MonoBehaviour
{
    [Header("References")]
    
    [SerializeField] GameObject debuglevelMenu;
    [SerializeField] GameObject debugMenu;
    [SerializeField] GameObject debugUpgradeMenu;

    public void DebugLevelMenu()
    {
        debuglevelMenu.SetActive(false);
        debuglevelMenu.SetActive(true);
    }
    

    public void DebugUpgradeMenu()
    {
        debugUpgradeMenu.SetActive(true);
        debugMenu.SetActive(false);
    }
}
