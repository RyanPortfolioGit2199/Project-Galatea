using UnityEngine;

public class ActiveThruster : MonoBehaviour
{
    [SerializeField] ThrusterSO thrusterSO;

    Thruster currentThruster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentThruster = GetComponentInChildren<Thruster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchThruster(ThrusterSO thrusterSO)
    {
        if (currentThruster)
        {
            Destroy(currentThruster.gameObject);
        }
        Thruster newThruster = Instantiate(thrusterSO.ThrusterPrefab, transform).GetComponent<Thruster>();
        currentThruster = newThruster;
        this.thrusterSO = thrusterSO;
    }
}
