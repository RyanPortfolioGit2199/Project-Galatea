using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
public static LevelManager Instance {get; private set;}

    [SerializeField] GameObject[] levelObjectives;
    [SerializeField] int levelObjectiveNeeded;
    
    // Update these numbers with the correct Scene Number for each Level Objective
    const int Level1 = 1;
    const int Level2 = 2;
    const int Level3 = 3;
    const int Level4 = 4;

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
    }

    void Start()
    {
        
    }

    void Update()
    {
        // This is just for debug will replace with a trigger from the start.
    }

    public void DebugLevelTrigger(int currentLevel)
    {
        // will replace later with GameManager version later

        levelObjectiveNeeded = currentLevel;
        InstantiateObjective();
    }

    public void LoadLevel(int levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void SetCurrentLevel(int level)
    {
        level += 1;
        SaveManager.Instance.UpdateLevel(level);
    }

    private void InstantiateObjective()
    {
        switch (levelObjectiveNeeded)
        {
            case Level1:
            // Instantiate the Level objective gameobject from the array
                Instantiate(levelObjectives[0]);
                Debug.Log("Level 1 Objective Loaded!");
                GameManager.Instance.TriggerEnemySpawning();
                break;
            case Level2:
            // Instantiate the Level objective gameobject from the array
                break;
            case Level3:
            // Instantiate the Level objective gameobject from the array
                break;
            case Level4:
            // Instantiate the Level objective gameobject from the array
                break;
        }
    }
}


