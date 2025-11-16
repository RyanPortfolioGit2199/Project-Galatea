using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class PauseManager : MonoBehaviour
{
    [SerializeField] bool isPaused;

    int mainMenuScene = 0;
    

    public void MenuStartUp(InputAction.CallbackContext context)
    {
        
    }

    public void OnPause()
    {
        Time.timeScale = 0;

    }

    public void ExitToMenu()
    {
        Debug.Log("Exit Back out to Main Menu");
        SceneManager.LoadScene(mainMenuScene);
    }
}
