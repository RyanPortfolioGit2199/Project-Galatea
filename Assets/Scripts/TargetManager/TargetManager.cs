using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetManager : MonoBehaviour
{
    public static TargetManager Instance { get; private set; }

    private Transform player;
    private Transform protectedObjective;
    private bool isProtectionLevel;

    private void OnEnable()
    {
        // Subscribe to Unity's sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called ONLY ONCE when a new scene finishes loading
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindWithTag("Player")?.transform;
        
        // Find the objective if it exists in this scene
        GameObject objectiveObj = GameObject.FindWithTag("ProtectedTarget");
        
        if (objectiveObj != null)
        {
            protectedObjective = objectiveObj.transform;
            isProtectionLevel = true;
        }
        else
        {
            protectedObjective = null;
            isProtectionLevel = false;
        }
    }

    public Vector3 GetCurrentTargetPosition(Vector3 enemyPosition)
    {
        if (!isProtectionLevel || protectedObjective == null)
        {
            return player != null ? player.position : Vector3.zero;
        }

        // Level 2 Logic: Evaluate between Player and Objective
        float distToPlayer = Vector3.Distance(enemyPosition, player.position);
        float distToObjective = Vector3.Distance(enemyPosition, protectedObjective.position);

        return (distToObjective < distToPlayer) ? protectedObjective.position : player.position;
    }
}
