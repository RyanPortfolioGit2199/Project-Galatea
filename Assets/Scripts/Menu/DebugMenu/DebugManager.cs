using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class DebugManager : MonoBehaviour
{
    [SerializeField] GameObject debugMenuUI;

    [SerializeField] bool debugEnabled = false;
    
    PlayerInputScript playerInputScript;
    PlayerInput playerInput;
    
    string debugMenu = "DebugMenu";
    int mainMenuLevel = 0;
    MainMenuUIHandler mainMenu;
    
    
    public static DebugManager Instance;

    private void Awake()
    {
        PersistBetweenScenes();
    }

    void Start()
    {
        
        playerInputScript = FindAnyObjectByType<PlayerInputScript>();
        mainMenu = FindFirstObjectByType<MainMenuUIHandler>();
        
        DebugController();
        

    }

    // Update is called once per frame
    void Update()
    {
        DebugController();
    }

    private void DebugController()
    {
        
        

        if (playerInputScript.debugMenu && !debugEnabled)
        {
            
            Debug.Log("Open Debug Menu");
            
            MainMenuConditional();
            debugMenuUI.SetActive(true);
            
            
        }

        StartCoroutine(ButtonPressDelay());
    
        if (playerInputScript.debugMenu && debugEnabled)
        {
            Debug.Log("Close Debug Menu");
            MainMenuConditional();
            debugMenuUI.gameObject.SetActive(false);
            debugEnabled = false;
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

    void PersistBetweenScenes()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator ButtonPressDelay()
    {
        yield return new WaitForSeconds(3f);
        debugEnabled = true;
    }
}
