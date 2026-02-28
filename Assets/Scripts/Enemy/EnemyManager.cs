using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Vector3 LastKnownPosition;
    public bool PlayerDetected;
    [SerializeField] float Radius = 10f;
    [SerializeField] List<GameObject> EnemiesInLevel;
    private AISensor sensor;
    private Coroutine timerRoutine;

    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AllEnemiesInLevel();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInSightCheck();
    }

    public void ReportPlayerLocation(Vector3 position)
    {
        PlayerDetected = true;
        LastKnownPosition = position;
    }

    public Vector3 RepositionLocation()
    {
        Vector3 newPlayerPosition = LastKnownPosition + Random.insideUnitSphere * Radius;

        return newPlayerPosition;
    }

    public void AllEnemiesInLevel()
    {
        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");

        EnemiesInLevel.AddRange(enemiesArray);

    }

    public void PlayerInSightCheck()
    {
        

        foreach (GameObject eObj in EnemiesInLevel)
        {
            sensor = eObj.GetComponent<AISensor>();
            if (!sensor.PlayerInSight && timerRoutine == null)
            {
                
                timerRoutine = StartCoroutine(StartTimer());
            }
            else if(sensor.PlayerInSight && timerRoutine != null)
            {
                StopCoroutine(timerRoutine);
                timerRoutine = null;
                
            }

            
        }


    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Logic executed after 3 seconds of all being false");
        timerRoutine = null;
        PlayerDetected = false;
    }
}
