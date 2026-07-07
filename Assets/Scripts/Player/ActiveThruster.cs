using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveThruster : MonoBehaviour
{
    [SerializeField] UpgradesSO thrusterSO;
    public bool canDodge = true;
    Thruster currentThruster;
    

    float timeSinceLastDodge;
    InputAction dodgeAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentThruster = GetComponentInChildren<Thruster>();
        
        dodgeAction = InputSystem.actions.FindAction("Dodge");
    }

    void Update()
    {
        CanDodge();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        HandleDodge();

    }

    private bool CanDodge()
    {
        timeSinceLastDodge += Time.deltaTime;

        if (timeSinceLastDodge >= thrusterSO.DodgeRechargeRate)
        {
            canDodge = true;
            Debug.Log("Dodge is available");
        }
        return canDodge;
    }

    private void HandleDodge()
    {
        

        if(!dodgeAction.IsPressed()) {return;}

        if (canDodge)
        {
            Debug.Log("Dodge");
            StartCoroutine(currentThruster.DodgeDuration(thrusterSO));
            timeSinceLastDodge = 0f;
            canDodge = false;
        }
        
    }

    public void SwitchThruster(UpgradesSO thrusterSO)
    {
        if (currentThruster)
        {
            Destroy(currentThruster.gameObject);
        }
        this.thrusterSO = null;
        this.thrusterSO = thrusterSO;
        Thruster newThruster = Instantiate(thrusterSO.ThrusterPrefab, transform).GetComponent<Thruster>();
        currentThruster = newThruster;       
    }
}
