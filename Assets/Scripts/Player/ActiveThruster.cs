using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveThruster : MonoBehaviour
{
    [SerializeField] ThrusterSO thrusterSO;

    Thruster currentThruster;
    

    float timeSinceLastDodge;
    InputAction dodgeAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentThruster = GetComponentInChildren<Thruster>();
        
        dodgeAction = InputSystem.actions.FindAction("Dodge");
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void HandleDodge()
    {
        timeSinceLastDodge += Time.deltaTime;

        if (timeSinceLastDodge >= thrusterSO.DodgeRechargeRate)
        {
            currentThruster.Dodge(thrusterSO);
        }
        timeSinceLastDodge = 0f;
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
