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
    
    
    int mainMenuLevel = 0;
    MainMenuUIHandler mainMenu;

    float buttonPressTimer = 2f;
    float buttonPressDelay = 2f;
    
    Scene currentScene;
    public static DebugManager Instance;

    private void Awake()
    {
        PersistBetweenScenes();

        
        ReferencesNeeded();
        
        
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Always unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This runs every single time a scene finishes loading
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Loaded scene: {scene.name}");
        // Put your scene-transition reset logic here
        ReferencesNeeded();
    }
private void ReferencesNeeded()
    {
        currentScene = SceneManager.GetActiveScene();
        playerInputScript = FindAnyObjectByType<PlayerInputScript>();
        if (currentScene.buildIndex != mainMenuLevel)
        {
            mainMenu = FindFirstObjectByType<MainMenuUIHandler>();
        }
    }
    void Start()
    {
        
        
        ReferencesNeeded();
        

    }

    

    // Update is called once per frame
    void Update()
    {

        if (currentScene.buildIndex != mainMenuLevel)
        {
            DebugController();
        }
        
    }

    private void DebugController()
    {
        buttonPressTimer += Time.deltaTime;

        if(!playerInputScript.debugMenu) {return;}

        if (buttonPressTimer >= buttonPressDelay && !debugEnabled)
        {
            
            Debug.Log("Open Debug Menu");
            
            debugMenuUI.SetActive(true);
            debugEnabled = true;
            buttonPressTimer = 0f;
        }

       
    
        if (buttonPressTimer >= buttonPressDelay && debugEnabled)
        {
            Debug.Log("Close Debug Menu");
            debugMenuUI.gameObject.SetActive(false);
            debugEnabled = false;
            buttonPressTimer = 0f;
        }

        
    }


    public void MainMenuOpen()
    {
        Debug.Log("Open Debug Menu");
            
        debugMenuUI.SetActive(true);
        debugEnabled = true;
        buttonPressTimer = 0f;

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

    
}
