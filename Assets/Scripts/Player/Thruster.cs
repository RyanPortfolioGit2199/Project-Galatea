using UnityEngine;


public class Thruster : MonoBehaviour
{
    [SerializeField] int dodgeAmount;

    PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dodge(ThrusterSO thrusterSO)
    {
        
    }
}
