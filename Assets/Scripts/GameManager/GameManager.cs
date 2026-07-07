using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // add a global upgradeScene variable for all other scripts later.
    // 1. Create the Singleton instance
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject levelCompletedMenu;
    [SerializeField]SpawnManager spawnManager;

    public int CurrentLevel;

    private void Awake()
    {
        // 2. Ensure only one GameManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        Time.timeScale = 1;
        
        Debug.Log(CurrentLevel);
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
        
    }
    void Start()
    {
        StartOfScene();
    }

    private void StartOfScene()
    {
        GetCurrentLevel();
        spawnManager = FindAnyObjectByType<SpawnManager>();
        if(spawnManager != null)
        {
            
        }
    }


    /*
        Methods the Overseeing what Levesls to get and what Levels to Set
    */

    private void GetCurrentLevel()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        if (File.Exists(filePath))
        {
            CurrentLevel = SaveManager.Instance.saveData.SavedLevel;
        }
        else
        {
            CurrentLevel = 0;
        }
    }


    public void SetNewGame()
    {
        CurrentLevel = 0;
        LevelManager.Instance.SetCurrentLevel(CurrentLevel);
        LevelManager.Instance.LoadLevel(SaveManager.Instance.saveData.SavedLevel);
        SaveManager.Instance.UpdateUpgrades(0, 0, 0);
        SaveManager.Instance.UpdateOwnedWeapons(0);
        SaveManager.Instance.UpdateOwnedShields(0);
        SaveManager.Instance.UpdateOwnedThrusters(0);
    }

    // Need a method like the one above but just for existing save data for the continue button


    public void ContinueGame()
    {
        LevelManager.Instance.LoadLevel(SaveManager.Instance.saveData.SavedLevel);
    }

    // 3. This is now a regular method, but called via the Instance

    /*

        Overseeing Methods that control the flow of Levels

    */
    public void TriggerGameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Game Over!!");
    }

    public void TriggerEnemySpawning()
    {
        spawnManager.SpawnTriggering();
    }

    public void TriggerLevelCompleted()
    {
        spawnManager.StopSpawning();
        levelCompletedMenu.SetActive(true);
        Time.timeScale = 0;
        // Add logic here to Save what level I completed and save my currency I got from the enemies in the level.

    }


}
