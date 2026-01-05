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
    string menuControlName = "MenuStartUp";




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
        

        
        if (playerInputScript.pause && !isPaused )
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(false);
        }
        else if (playerInputScript.pause  && isPaused)
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
