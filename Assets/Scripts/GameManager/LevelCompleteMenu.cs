using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{
    int mainMenuScene = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void ExitToMenu()
    {
        Debug.Log("Exit Back out to Main Menu");
        SceneManager.LoadScene(mainMenuScene);
    }

    public void ContinueToUpgradeMenu()
    {
        Debug.Log("To the Upgrade Menu");
    }
}
