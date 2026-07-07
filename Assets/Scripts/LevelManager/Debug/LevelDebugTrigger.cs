using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDebugTrigger : MonoBehaviour
{
    public int levelNumber;
    [SerializeField] LevelManager levelManager;

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
    private void StartOfScene()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnParticleCollision(GameObject other)
    {
        levelManager.DebugLevelTrigger(levelNumber);
        this.gameObject.SetActive(false);
    }
}
