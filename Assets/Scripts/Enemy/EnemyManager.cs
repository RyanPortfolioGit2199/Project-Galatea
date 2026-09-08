using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("Detection & Tracking")]
    public Vector3 LastKnownPosition;
    public bool PlayerDetected;
    [SerializeField] private float Radius = 10f;

    [Header("Level 2 Protection Objective")]
    [SerializeField] private Transform protectedObjective;
    [SerializeField] private bool hasProtectedObjective;

    [Header("Active Level Enemies")]
    [SerializeField] private List<GameObject> EnemiesInLevel = new List<GameObject>();

    private Transform playerTransform;
    private AISensor sensor;
    private Coroutine timerRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        // Subscribe to scene load events so manager handles scene switches automatically
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        InitSceneReferences();
    }

    private void Update()
    {
        PlayerInSightCheck();
    }

    /// <summary>
    /// Resets enemy lists and updates target references every time a new scene loads.
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitSceneReferences();
    }

    private void InitSceneReferences()
    {
        // 1. Reset Enemy List for new level
        EnemiesInLevel.Clear();
        AllEnemiesInLevel();

        // 2. Cache Player Transform
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            LastKnownPosition = playerTransform.position;
        }

        // 3. Auto-detect if Level 2 Protected Objective exists in scene
        GameObject objectiveObj = GameObject.FindWithTag("ProtectedTarget");
        if (objectiveObj != null)
        {
            protectedObjective = objectiveObj.transform;
            hasProtectedObjective = true;
        }
        else
        {
            protectedObjective = null;
            hasProtectedObjective = false;
        }
    }

    /// <summary>
    /// Core method for enemies to request their primary destination target based on the active level.
    /// </summary>
    public Vector3 GetCurrentTargetPosition(Vector3 enemyPosition)
    {
        // Level 1 Behavior: No objective present -> Always return LastKnownPosition/Player
        if (!hasProtectedObjective || protectedObjective == null)
        {
            return LastKnownPosition;
        }

        // Level 2 Behavior: Evaluate distance between Player and Objective
        if (playerTransform == null) return protectedObjective.position;

        float distToPlayer = Vector3.Distance(enemyPosition, playerTransform.position);
        float distToObjective = Vector3.Distance(enemyPosition, protectedObjective.position);

        // Target whichever is closer (or add custom priority weighting here)
        return (distToObjective < distToPlayer) ? protectedObjective.position : LastKnownPosition;
    }

    public void ReportPlayerLocation(Vector3 position)
    {
        PlayerDetected = true;
        LastKnownPosition = position;
    }

    /// <summary>
    /// Calculates a reposition point relative to the active target position.
    /// </summary>
    public Vector3 RepositionLocation(Vector3 enemyPosition)
    {
        Vector3 targetPos = GetCurrentTargetPosition(enemyPosition);
        Vector3 newTargetPosition = targetPos + Random.insideUnitSphere * Radius;
        return newTargetPosition;
    }

    public void AllEnemiesInLevel()
    {
        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
        EnemiesInLevel.AddRange(enemiesArray);
    }

    public void PlayerInSightCheck()
    {
        if (EnemiesInLevel.Count == 0) return;

        bool anyPlayerInSight = false;

        foreach (GameObject eObj in EnemiesInLevel)
        {
            if (eObj != null)
            {
                sensor = eObj.GetComponent<AISensor>();
                if (sensor != null && sensor.PlayerInSight)
                {
                    anyPlayerInSight = true;
                    break;
                }
            }
        }

        if (!anyPlayerInSight && timerRoutine == null)
        {
            timerRoutine = StartCoroutine(StartTimer());
        }
        else if (anyPlayerInSight && timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
            timerRoutine = null;
            PlayerDetected = true;
        }
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("No enemies see the player. Resetting PlayerDetected.");
        timerRoutine = null;
        PlayerDetected = false;
    }
}