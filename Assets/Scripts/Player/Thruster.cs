using System.Collections;
using UnityEngine;



public class Thruster : MonoBehaviour
{
    PlayerController playerController;

    CharacterController characterController;

    [SerializeField] float dodgeDurationAmount;
    
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

 

    public IEnumerator DodgeDuration(UpgradesSO thrusterSO)
    {
        float time = 0;
        while(time < dodgeDurationAmount)
        {
            characterController.Move(playerController.moveDirection * thrusterSO.dodgeAmount * Time.deltaTime);
            time += Time.deltaTime;
            
            yield return null;
        }
             
    }
}
