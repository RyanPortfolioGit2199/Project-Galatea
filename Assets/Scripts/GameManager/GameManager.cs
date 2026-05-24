using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 1. Create the Singleton instance
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject levelCompletedMenu;
    [SerializeField] GameObject spawnManager;

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
    }

    // 3. This is now a regular method, but called via the Instance
    public void TriggerGameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Game Over!!");
    }

    public void TriggerEnemySpawning()
    {
        spawnManager.SetActive(true);
    }

    public void TriggerLevelCompleted()
    {
        levelCompletedMenu.SetActive(true);
        Time.timeScale = 0;
        // Add logic here to Save what level I completed and save my currency I got from the enemies in the level.

    }
}
