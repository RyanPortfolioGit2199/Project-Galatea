using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveWeapon : MonoBehaviour
{
    Gun currentGun;
    float timeSinceLastShot = 0f;

    PlayerInputScript playerInputScript;


    [Header("References")]
    public WeaponSO WeaponSO;

    

    public bool isFiring = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGun = GetComponentInChildren<Gun>();
        playerInputScript = GetComponentInParent<PlayerInputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        HandleShoot();
    }

    void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if(!playerInputScript.shoot)  return;

        if(timeSinceLastShot >= WeaponSO.FireRate)
        {
            
            currentGun.Shoot(WeaponSO);
            timeSinceLastShot = 0f;
        }

    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentGun)
        {
            Destroy(currentGun.gameObject);
        }
        Gun newGun = Instantiate(WeaponSO.GunPrefab, transform).GetComponent<Gun>();
        currentGun = newGun;
// this.weaponSO is the weaponSO variable declared at the begining of the script. ///// the other weaponSO is the on I declared at the start of the SwitchWeapon Method
        this.WeaponSO = weaponSO;
    }
}
