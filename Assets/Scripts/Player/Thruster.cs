using System.Collections;
using UnityEngine;


public class Thruster : MonoBehaviour
{
    PlayerController playerController;

    Rigidbody playerRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponentInParent<Rigidbody>();
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dodge(ThrusterSO thrusterSO)
    {
        
        if(playerController.moveDirection.x > 0 || playerController.moveDirection.x < 0 )
        {
            playerRb.linearVelocity = transform.right * playerController.moveDirection.x * thrusterSO.dodgeAmount * Time.fixedDeltaTime;
        }
        else if (playerController.moveDirection.z > 0 || playerController.moveDirection.z < 0)
        {
            playerRb.linearVelocity = transform.forward * playerController.moveDirection.z * thrusterSO.dodgeAmount * Time.fixedDeltaTime;
        }
        
        
        StartCoroutine(DodgeDuration());
    }

    IEnumerator DodgeDuration()
    {
        yield return new WaitForSeconds(.5f);
        playerRb.linearVelocity = Vector3.zero;
    }
}
