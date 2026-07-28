using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class ActiveWeapon : MonoBehaviour
{
    Gun currentGun;
    float timeSinceLastShot = 0f;
    private bool isTriggerHeld = false;

    Scene currentScene;
    int UpgradeScene = 3; // Change if the UpgradeScene gets changed in the Scene List


    [Header("References")]
    public UpgradesSO upgradesSO;
    [SerializeField]PlayerInputScript playerInputScript;
    

    public bool isFiring = false;

    void Awake()
    {
        currentGun = GetComponentInChildren<Gun>();
        currentScene = SceneManager.GetActiveScene();
    }

    void OnEnable()
    {
        playerInputScript.OnFireContextChanged += HandleFireInput;
    }

    void OnDisable()
    {
        playerInputScript.OnFireContextChanged -= HandleFireInput;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        InitializeWeapon();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // HARD GUARD: If not automatic, Update DOES NOTHING and returns immediately!
        if (upgradesSO == null || !upgradesSO.IsAutomatic) return;
        AutomaticWeapons();

    }

    private void AutomaticWeapons()
    {
        if(currentScene.buildIndex == UpgradeScene) return;// makes it sure that the player can move in the Upgrade menu.
        if (isTriggerHeld && Time.time >= timeSinceLastShot)
        {
            timeSinceLastShot = Time.time + upgradesSO.FireRate;
            currentGun.Shoot(upgradesSO);
        }
    }

    void HandleFireInput(InputAction.CallbackContext context)
    {
        if(upgradesSO == null){return;}
        if(currentScene.buildIndex == UpgradeScene) return;// makes it sure that the player can move in the Upgrade menu.

        if(context.started)
        {
            isTriggerHeld = true;

            if(!upgradesSO.IsAutomatic)
            {
                if(Time.time >= timeSinceLastShot)
                {
                    timeSinceLastShot = Time.time + upgradesSO.FireRate;
                    currentGun.Shoot(upgradesSO);
                }
            }
        }
        else if (context.canceled)
        {
            isTriggerHeld = false;
        }

    }

    public void InitializeWeapon()
    {
        if(upgradesSO == null){return;}
        isTriggerHeld = false;
    }


    public void SwitchWeapon(UpgradesSO upgradeWeaponSO)
    {
        
        Destroy(currentGun.gameObject);
          
        this.upgradesSO = upgradeWeaponSO;
        Gun newGun = Instantiate(upgradesSO.GunPrefab, transform).GetComponent<Gun>();
        currentGun = newGun;
// this.weaponSO is the weaponSO variable declared at the begining of the script. ///// the other weaponSO is the on I declared at the start of the SwitchWeapon Method
        
    }
}
