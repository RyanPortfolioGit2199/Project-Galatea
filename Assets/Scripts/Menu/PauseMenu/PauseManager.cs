using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;


public class PauseManager : MonoBehaviour
{
    [SerializeField] bool isPaused;

    InputAction pauseAction;

    int mainMenuScene = 0;
    string menuControlName = "MenuStartUp";




    private void Start()
    {
        pauseAction = InputSystem.actions.FindAction(menuControlName);
    }

    private void Update()
    {
        OnPause();
    }

    

    public void OnPause()
    {
        if (pauseAction.WasReleasedThisFrame() && !isPaused)
        {
            isPaused = true;
        }
        else if (pauseAction.WasReleasedThisFrame() && isPaused)
        {
            isPaused = false;
        }

    }

    public void ExitToMenu()
    {
        Debug.Log("Exit Back out to Main Menu");
        SceneManager.LoadScene(mainMenuScene);
    }
}
