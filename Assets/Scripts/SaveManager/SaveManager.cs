using UnityEngine;
using System.IO;


    [System.Serializable]
    
 
    public class SaveData
    {
        public int SavedLevel;
        public int SavedCurrency;
        public int SavedPlayerWeapon;
        public int SavedPlayerShield;
        public int SavedPlayerThruster;
    }
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance {get; private set;}

    public SaveData saveData = new SaveData();
    private string savePath;

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

        savePath = Path.Combine(Application.persistentDataPath, "savefile.json");

        LoadGame();
    }
    

    public void UpdateLevel()
    {
        SaveGame();
    }

    public void UpdateUpgrades(int weapon, int shield, int thruster)
    {
        SaveGame();
    }

    public void UpdateCurrency(int amount)
    {
        saveData.SavedCurrency = amount;
        SaveGame();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
    }
}
