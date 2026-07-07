using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] Spawners;

    [Header("Enemy Types")]
    [SerializeField] GameObject gruntPrefab;
    [SerializeField] GameObject brutePrefab;
    [SerializeField] GameObject sniperPrefab;

    [Header("Spawning Settings")]
    [SerializeField] private int spawnerNumber;
    [SerializeField] private float spawnTime;
    [SerializeField] private float startDelay;
    [SerializeField] [Range(0, 1)] float gruntSpawnChance;
    [SerializeField] [Range(0, 1)] float bruteSpawnChance;
    [SerializeField] [Range(0, 1)] float sniperSpawnChance;
    
    public int maxEnemies = 5;
    public int enemyCount;
    public bool canSpawn;

    private float spawnTimer;

    public
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTime)
        {

        }

        // need to replace later to add the enemies spawned to a list to save on performance and possiblly used as a fail safe on to many enemies of 1 type being spawned when I dont want it too.
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length; // finds the amount(Length) of enemies in the scene 
    }

    public void SpawnTriggering()
    {
        InvokeRepeating(nameof(SpawningMethods), startDelay, spawnTime);
    }

    public void StopSpawning()
    {
        CancelInvoke();
    }

    public void SpawningMethods()
    {
        RandomSpawner();
        SpawnGrunt();
        SpawnBrute();
        SpawnSniper();       
    }

    int RandomSpawner() // Did this in a seperate method for optimization purposes (not needed because its a small calculation but better to get into the mindset) to only generate a random spawner location when its needed to spwan an enemy.
    {
        if (enemyCount <= maxEnemies)
        {
            spawnerNumber = Random.Range(0, Spawners.Length);
        } // Generate a number between 0 and the Arrays length and returns which Spawner's number in the array for the enemy to spawn to.
        return spawnerNumber;
    }

/*
    Replace later with

    * with a separate Spawn method for each Enemy Type based on a percentage chance in the design 
    
*/
    void SpawnGrunt()
    {
        if(Random.value > gruntSpawnChance) return;

        if (enemyCount <= maxEnemies)
        {
            Instantiate(gruntPrefab, Spawners[spawnerNumber].transform.position, Spawners[spawnerNumber].transform.rotation);
            
        }
        spawnTimer = 0;
    }

    void SpawnBrute()
    {
        if(Random.value > bruteSpawnChance) return;

        if (enemyCount <= maxEnemies)
        {
            Instantiate(brutePrefab, Spawners[spawnerNumber].transform.position, Spawners[spawnerNumber].transform.rotation);
            
        }
        spawnTimer = 0;
    }

    void SpawnSniper()
    {
        if(Random.value > sniperSpawnChance) return;

        if (enemyCount <= maxEnemies)
        {
            Instantiate(sniperPrefab, Spawners[spawnerNumber].transform.position, Spawners[spawnerNumber].transform.rotation);
            
        }
        spawnTimer = 0;
    }

    
}
