using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Vector3 LastKnownPosition;
    public bool PlayerDetected;

    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReportPlayerLocation(Vector3 position)
    {
        PlayerDetected = true;
        LastKnownPosition = position;
    }
}
