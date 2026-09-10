using UnityEngine;
using UnityEngine.AI;

public class TransportShip : MonoBehaviour
{
    ObstacleAgent ObstacleAgent;
    StateMachine brain;
    NavMeshAgent agent;
    TransportSensor sensor;
    Rigidbody shipRb;

    [SerializeField] GameObject targetDestination;
    [SerializeField] private float speedReductionPerObject = 1.5f;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 6f;
    float targetSpeed;

    void Awake()
    {
        brain = GetComponent<StateMachine>();
        ObstacleAgent = GetComponent<ObstacleAgent>();
        sensor = GetComponent<TransportSensor>();
        agent = GetComponent<NavMeshAgent>();
        shipRb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpeedController();
    }

    public void ShipMovementTriggered()
    {
        EnemyManager.Instance.ReportPlayerLocation(this.transform.position);
        brain.PushState(OnTravelingEnter, OnTraveling, OnTravelingExit);
    }

    void OnTravelingEnter()
    {
        
    }
    void OnTraveling()
    {
        ObstacleAgent.SetDestination(targetDestination.transform.position);
    }

    void OnTravelingExit()
    {
        
    }

    void SpeedController()
    {
        agent.speed = CalculateSpeed();
    }

    float CalculateSpeed()
    {
        //Reduce base speed based on object count
        targetSpeed = maxSpeed - (sensor.enemyCount * speedReductionPerObject);

        // Clamp the speed between minimum and maximum safe boundaries
        return Mathf.Clamp(targetSpeed, minSpeed, maxSpeed);
    }
}
