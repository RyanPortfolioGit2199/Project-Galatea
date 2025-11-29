using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugLevelUIHandler : MonoBehaviour
{
    [SerializeField] GameObject debugLevelMenu;

    int playerDbLevel = 1;

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
    }

    public void ExitToDebugMenu()
    {
        Debug.Log("Exit Back out DebugMenu");
        
    }
}
