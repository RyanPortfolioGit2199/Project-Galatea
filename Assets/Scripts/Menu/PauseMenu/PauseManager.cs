using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;



public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [SerializeField] bool isPaused;


    [SerializeField] GameObject pauseMenu;


    PlayerInputScript playerInputScript;
    int mainMenuScene = 0;
    

    float buttonPressTimer = 1f;
    float buttonPressDelay = 1f;



    private void Start()
    {   
        playerInputScript = FindAnyObjectByType<PlayerInputScript>();
    }

    

    private void Update()
    {
        OnPause();
    }

    

    public void OnPause()
    {
        buttonPressTimer += Time.unscaledDeltaTime; // if I used .deltaTime it wouldnt run since the Time Scale is being set to 0

        if (!playerInputScript.pause) {return;}
        
        if ( buttonPressTimer >= buttonPressDelay && !isPaused)
        {
            
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            buttonPressTimer = 0; 
            isPaused = true;
        }

        if (buttonPressTimer >= buttonPressDelay && isPaused)
        {
            
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            buttonPressTimer = 0; 
            isPaused = false;
        }
    

    }

    public void ExitToMenu()
    {
        Debug.Log("Exit Back out to Main Menu");
        SceneManager.LoadScene(mainMenuScene);
    }
}
