using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 1. Create the Singleton instance
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverMenu;

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

        Time.timeScale = 1;
    }

    // 3. This is now a regular method, but called via the Instance
    public void TriggerGameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Game Over!!");
    }
}
