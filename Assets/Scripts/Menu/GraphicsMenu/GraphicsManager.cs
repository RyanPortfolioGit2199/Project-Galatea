using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GraphicsManager : MonoBehaviour
{
    public static GraphicsManager Instance {get; private set;}

    [SerializeField] GameObject graphicsMenu;

    const int Resolution1 = 0;
    const int Resolution2 = 1;
    const int Resolution3 = 2;

    const int MaxFPS1 = 0;
    const int MaxFPS2 = 1;
    const int MaxFPS3 = 2;
    const int MaxFPS4 = 3;
    const int MaxFPS5 = 4;

    public int setResolution {get; private set;}
    public int setMaxFPS {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        
        StartOfScene();
        SetSavedResolution();
        SetSavedMaxFPS();
        SetSaveVsync();
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Loaded scene: {scene.name}");
        // Put your scene-transition reset logic here
        StartOfScene();    
        //GainedCurrency(SaveManager.Instance.saveData.SavedCurrency);
    }
    private void StartOfScene()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GraphicsMenuToggle(bool canEnable)
    {
        if(canEnable == true)
        {
            graphicsMenu.SetActive(true);
        }
        else if(canEnable == false)
        {
            graphicsMenu.SetActive(false);
        }
    }


    /*
    Need to look into not being able to set Resolution your monitor doesn't support;
    */

    public void SetResolution(int resolutionID)
    {
        switch (resolutionID)
        {
            case Resolution1:
            Screen.SetResolution(1280, 720, true);
            break;

            case Resolution2:
            Screen.SetResolution(1920, 1080, true);
            break;

            case Resolution3:
            Screen.SetResolution(2560, 1440, true);
            break;
            
        }

        SaveManager.Instance.UpdateResolution(resolutionID);
    }

    public void ToggleVerticalSync(bool toggleValue)
    {
        if (toggleValue)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        SaveManager.Instance.UpdateVsyncToggle(toggleValue);
    }

    public void MaxFPSController(int maxFpsID)
    {
        switch (maxFpsID)
        {
            case MaxFPS1:
            Application.targetFrameRate = -1;
            break;

            case MaxFPS2:
            Application.targetFrameRate = 30;
            break;

            case MaxFPS3:
            Application.targetFrameRate = 60;
            break;

            case MaxFPS4:
            Application.targetFrameRate = 90;
            break;

            case MaxFPS5:
            Application.targetFrameRate = 120;
            break;
            
        }

        SaveManager.Instance.UpdateMaxFPS(maxFpsID);
    }



    public void SetSavedResolution()
    {
        SetResolution(SaveManager.Instance.settingsSaveData.SavedResolution);
    }

    public void SetSaveVsync()
    {
        ToggleVerticalSync(SaveManager.Instance.settingsSaveData.SavedVsync);
    }

    public void SetSavedMaxFPS()
    {
        MaxFPSController(SaveManager.Instance.settingsSaveData.SavedFPS);
    }

}
