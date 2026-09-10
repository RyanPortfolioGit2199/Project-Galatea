using UnityEngine;

public class LevelTwoObjective : ObjectiveScripte
{
    TransportShip ship;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        TargetDestination.onDestinationReached += TargetReached;
    }

    void OnDisable()
    {
        TargetDestination.onDestinationReached -= TargetReached;
    }

    void Awake()
    {
        ship = FindAnyObjectByType<TransportShip>();
    }

    void Start()
    {
        ship.ShipMovementTriggered();
    }

    void TargetReached()
    {
        FinishObjectiveStep();
    }
}
