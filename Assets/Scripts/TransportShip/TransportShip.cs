using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class TransportShip : MonoBehaviour
{
    ObstacleAgent ObstacleAgent;
    StateMachine brain;
    NavMeshAgent agent;
    TransportSensor sensor;


    [SerializeField] SplineContainer splineContainer;
    [SerializeField] private bool loopPath = true;
    [SerializeField] private float arrivalDistance = 1.0f;
    [SerializeField] GameObject targetDestination;
    [SerializeField] private float speedReductionPerObject = 1.5f;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 6f;
    float targetSpeed;
    float progress = 0f;
    float splineLength;
    Vector3 localPos;
    Vector3 worldPos;

    void Awake()
    {
        brain = GetComponent<StateMachine>();
        ObstacleAgent = GetComponent<ObstacleAgent>();
        sensor = GetComponent<TransportSensor>();
        agent = GetComponent<NavMeshAgent>();

        if(splineContainer != null)
        {
            splineLength = splineContainer.CalculateLength();
        }
        
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
        //ObstacleAgent.SetDestination(targetDestination.transform.position);
        AdvanceTargetPosition();
    }

    void OnTravelingExit()
    {
        
    }

    void AdvanceTargetPosition()
    {
        progress += (arrivalDistance / splineLength);

        if (progress >= 1f)
        {
            if(loopPath)
                progress %= 1f;
            else
                return;
        }
        MoveToNextSplineTarget();
    }

    void MoveToNextSplineTarget()
    {
        localPos = splineContainer.EvaluatePosition(progress);
        worldPos = splineContainer.transform.TransformPoint(localPos);
        ObstacleAgent.SetDestination(worldPos);
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
