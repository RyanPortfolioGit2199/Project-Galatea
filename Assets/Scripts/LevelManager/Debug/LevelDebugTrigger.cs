using UnityEngine;

public class LevelDebugTrigger : MonoBehaviour
{
    public int levelNumber;
    [SerializeField] LevelManager levelManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnParticleCollision(GameObject other)
    {
        levelManager.DebugLevelTrigger(levelNumber);
        this.gameObject.SetActive(false);
    }
}
