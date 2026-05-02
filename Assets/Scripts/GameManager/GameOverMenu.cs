using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    int mainMenuScene = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitToMenu()
    {
        Debug.Log("Exit Back out to Main Menu");
        SceneManager.LoadScene(mainMenuScene);
    }

    public void RetryLevel()
    {
        Debug.Log("Retrying Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
