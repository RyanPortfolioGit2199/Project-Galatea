using UnityEngine;

public class TransportSensor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LayerMask enemyLayers;

    [Header("Sensor Control")]
    [SerializeField] float sensorSize = 10f;
    [SerializeField] float scanFrequency = 0.5f;
    float scanInterval;
    float scanTimer;
    public int enemyCount;

    Collider[] enemyColliders = new Collider[10];

    [Header("Debug Toggle")]
    [SerializeField] bool showSensor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scanInterval = 1.0f/scanFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        scanTimer -= Time.deltaTime;
        if(scanTimer < 0)
        {
            scanTimer += scanInterval;
            EnemyScan();            
        }
    }

    void EnemyScan()
    {
        enemyCount = Physics.OverlapSphereNonAlloc(transform.position, sensorSize, enemyColliders, enemyLayers, QueryTriggerInteraction.Collide);
    }

    private void OnDrawGizmos()
    {
        if(this.gameObject != null)
        {
            if (showSensor)
            {
                Gizmos.DrawSphere(transform.position, sensorSize);
            }
        }
        
    }
}
