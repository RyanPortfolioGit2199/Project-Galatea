using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveWeapon : MonoBehaviour
{
    Gun currentGun;
    float timeSinceLastShot = 0f;


    [Header("References")]
    [SerializeField] WeaponSO weaponSO;

    InputAction shootAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGun = GetComponentInChildren<Gun>();
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if(!shootAction.IsPressed()) return;

        if(timeSinceLastShot >= weaponSO.FireRate)
        {
            currentGun.Shoot(weaponSO);
        }
    }
}
