using UnityEngine;

public class ActiveShield : MonoBehaviour
{
    [SerializeField] ShieldSO shieldSO;

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

    public void SwitchShield(ShieldSO shieldSO)
    {
        if (currentShield)
        {
            Destroy(currentShield.gameObject);
        }
        Shield newShield = Instantiate(shieldSO.ShieldPrfab, transform).GetComponent<Shield>();
        currentShield = newShield;
        this.shieldSO = shieldSO;
    }
}
