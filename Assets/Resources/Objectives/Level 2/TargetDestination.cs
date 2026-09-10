using UnityEngine;

public class TargetDestination : MonoBehaviour
{
    public delegate void OnDestinationReached();
    public static OnDestinationReached onDestinationReached;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProtectedTarget"))
        {
            onDestinationReached?.Invoke();

            Debug.Log("Transport Ship Arrived at Destination");
        }
    }
}
