using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugLevelUIHandler : MonoBehaviour
{
    [SerializeField] GameObject debugLevelMenu;

    int playerDbLevel = 1;
    int upgradesLevel = 2;

    public void PlayerDBLevel()
    {
        Debug.Log("Load Player Debug Level");
        SceneManager.LoadScene(playerDbLevel);
        debugLevelMenu.SetActive(false);
    }

    public void EnemyDebugLevel()
    {
        Debug.Log("Load Enemy Debug Level");
    }

    public void UpgradeMenuDBLevel()
    {
        Debug.Log("Upgrade Menu Debug Level");
        SceneManager.LoadScene(upgradesLevel);
        debugLevelMenu.SetActive(false);
    }

    public void ExitToDebugMenu()
    {
        Debug.Log("Exit Back out DebugMenu");
        
    }
}
