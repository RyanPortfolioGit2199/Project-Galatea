using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class Thruster : MonoBehaviour
{
    PlayerController playerController;

    CharacterController characterController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        playerController = GetComponentInParent<PlayerController>();
        characterController = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 

    public IEnumerator DodgeDuration(ThrusterSO thrusterSO)
    {
        
        
        yield return new WaitForSeconds(5f);
        characterController.Move(playerController.moveDirection * thrusterSO.dodgeAmount * Time.deltaTime);
    }
}
