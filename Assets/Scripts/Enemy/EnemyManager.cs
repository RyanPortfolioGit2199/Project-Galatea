using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("Detection & Tracking")]
    public Vector3 PlayerLastKnownPosition;
    public Vector3 TransportShipLastKnownPosition;
    public bool PlayerDetected;
    [SerializeField] private float Radius = 10f;

    [Header("Level 2 Protection Objective")]
    [SerializeField] private Transform protectedObjective;
    public bool hasProtectedObjective;

    [Tooltip("Maximum number of enemies allowed to target the Transport Ship at once.")]
    [SerializeField] private int maxShipAttackers = 3;
    
    // Tracks which enemies are currently targeting the ship
    public HashSet<Enemy> shipAttackers = new HashSet<Enemy>();

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
            PlayerLastKnownPosition = playerTransform.position;
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
    public Vector3 GetCurrentTargetPosition(Enemy requestingEnemy)
    {
        Vector3 enemyPos = requestingEnemy.transform.position;
        if (!hasProtectedObjective || protectedObjective == null)
        {
            ReleaseShipSlot(requestingEnemy);
            return PlayerLastKnownPosition;
        }

        // Level 2: Evaluate target choice
        if (playerTransform == null) return protectedObjective.position;

        float distToPlayer = Vector3.Distance(enemyPos, PlayerLastKnownPosition);
        float distToObjective = Vector3.Distance(enemyPos, protectedObjective.position);

        // Check if this enemy is ALREADY registered as attacking the ship
        bool isAlreadyAttackingShip = shipAttackers.Contains(requestingEnemy);

        //Case A: Enemy wants to target ship because it is closer to the ship than the player
        if(distToObjective <= distToPlayer)
        {
            //if enemy is already assigned a slot OR if there is an available slot:
            if (isAlreadyAttackingShip || shipAttackers.Count < maxShipAttackers)
            {
                shipAttackers.Add(requestingEnemy); // Claim slot
                return protectedObjective.position;
            }
        }

        // Case B: Ship slots are FULL or enemy is closer to the Player
        ReleaseShipSlot(requestingEnemy); // Free Up slot if it previously had one
        Debug.Log("Transport Ship is full moving to player");
        return PlayerLastKnownPosition;
    }

    /// <summary>
    /// Call this when an enemy dies or stops attacking the ship to free up a slot.
    /// </summary>
    public void ReleaseShipSlot(Enemy enemy)
    {
        if (shipAttackers.Contains(enemy))
        {
            shipAttackers.Remove(enemy);
        }
    }

    public void ReportPlayerLocation(Vector3 position)
    {
        PlayerDetected = true;
        PlayerLastKnownPosition = position;
        Debug.Log("Player Detected");
    }

    /// <summary>
    /// Calculates a reposition point relative to the active target position.
    /// </summary>
    public Vector3 RepositionLocation(Enemy requestingEnemy)
    {
        Vector3 targetPos = GetCurrentTargetPosition(requestingEnemy);
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