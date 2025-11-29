using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DebugManager : MonoBehaviour
{

    [SerializeField] bool debugEnabled = false;

    InputAction debugAction;
    string debugMenu = "DebugMenu";
    int mainMenuLevel = 0;
    MainMenuUIHandler mainMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        debugAction = InputSystem.actions.FindAction(debugMenu);

        mainMenu = FindFirstObjectByType<MainMenuUIHandler>();

       

    }

    // Update is called once per frame
    void Update()
    {
        DebugController();
    }

    private void DebugController()
    {
        if (debugAction.WasReleasedThisFrame() && !debugEnabled)
        {
            Debug.Log("Open Debug Menu");
            debugEnabled = true;
            MainMenuConditional();
        }
        else if (debugAction.WasReleasedThisFrame() && debugEnabled)
        {
            Debug.Log("Close Debug Menu");
            debugEnabled = false;
            MainMenuConditional();
        }
    }


    void MainMenuConditional()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex != mainMenuLevel) { return; }

        if (!debugEnabled)
        {
            mainMenu.gameObject.SetActive(true);
        }
        else if (debugEnabled)
        {
            mainMenu.gameObject.SetActive(false);
        }
    }
}
