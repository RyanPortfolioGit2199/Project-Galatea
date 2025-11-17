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



    InputAction pauseAction;

    int mainMenuScene = 0;
    string menuControlName = "MenuStartUp";




    private void Awake()
    {
        
        

        pauseAction = InputSystem.actions.FindAction(menuControlName);

        
    }

    

    private void Update()
    {
        OnPause();
    }

    

    public void OnPause()
    {
        


        if (pauseAction.WasReleasedThisFrame() && !isPaused )
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(false);
        }
        else if (pauseAction.WasReleasedThisFrame() && isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(true);
        }

    }

    public void ExitToMenu()
    {
        Debug.Log("Exit Back out to Main Menu");
        SceneManager.LoadScene(mainMenuScene);
    }
}
