using UnityEngine;
using TMPro;
using System.IO;

public class CurrencyManager : MonoBehaviour
{

    public static CurrencyManager Instance {get; private set;}

    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyText;

    public int currency;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

        
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GainedCurrency(SaveManager.Instance.SavedCurrency);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainedCurrency(int amount)
    {
        currency += amount;
        currencyText.SetText("$ " + currency);
    }
}
