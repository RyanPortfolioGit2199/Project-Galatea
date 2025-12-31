using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenuUIHandler : MonoBehaviour
{
    [Header("References")]
    
    [SerializeField] GameObject debuglevelMenu;
    [SerializeField] GameObject debugMenu;

    public void DebugLevelMenu()
    {
        debuglevelMenu.SetActive(false);
        debuglevelMenu.SetActive(true);
    }
}
