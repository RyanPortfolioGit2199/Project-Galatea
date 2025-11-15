using UnityEngine;

public class DebugMenuUIHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject debugMenu;

    public void PlayerDBLevel()
    {
        Debug.Log("Load Player Debug Level");
    }

    public void EnemyDebugLevel()
    {
        Debug.Log("Load Enemy Debug Level");
    }

    public void UpgradeMenuDBLevel()
    {
        Debug.Log("Upgrade Menu Debug Level");
    }

    public void ExitToMenu()
    {
        Debug.Log("Exit Back out to Main Menu");
        debugMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
