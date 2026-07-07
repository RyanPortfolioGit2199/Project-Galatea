using UnityEngine;

public class ActiveShield : MonoBehaviour
{
    public UpgradesSO shieldSO;
    

    Shield currentShield;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentShield = GetComponentInChildren<Shield>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchShield(UpgradesSO shieldSO)
    {
        if (currentShield)
        {
            Destroy(currentShield.gameObject);
        }
        Shield newShield = Instantiate(shieldSO.ShieldPrefab, transform).GetComponent<Shield>();
        currentShield = newShield;
        this.shieldSO = shieldSO;
    }
}
