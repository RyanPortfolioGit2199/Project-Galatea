using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceHealthBar : MonoBehaviour
{
    
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offsetPosition;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        
        transform.rotation = Camera.main.transform.rotation;
        transform.position =  target.position + offsetPosition;
        
    }
}
