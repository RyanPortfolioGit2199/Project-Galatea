using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class CurrencyManager : MonoBehaviour
{

    public static CurrencyManager Instance {get; private set;}

    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyText;
    [SerializeField] Canvas textObject;

    Scene currentScene;
    int mainMenuScene;

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

        

        currentScene = SceneManager.GetActiveScene();

        if(currentScene.buildIndex == mainMenuScene)
        {
            currencyText.enabled = false;
        }
        else
        {
            currencyText.enabled = true;
        }
        

    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Always unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This runs every single time a scene finishes loading
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Loaded scene: {scene.name}");
        // Put your scene-transition reset logic here

        currentScene = SceneManager.GetActiveScene();

        if(currentScene.buildIndex == mainMenuScene)
        {
            currencyText.enabled = false;
        }
        else
        {
            currencyText.enabled = true;
        }
        
        GainedCurrency(SaveManager.Instance.saveData.SavedCurrency);
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GainedCurrency(SaveManager.Instance.saveData.SavedCurrency);
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
