using UnityEngine;
using System.IO;
using System.Collections.Generic;


[System.Serializable]
    
 
    public class SaveData
    {
        public int SavedLevel;
        public float SavedCurrency;
        public int SavedPlayerWeapon;
        public int SavedPlayerShield;
        public int SavedPlayerThruster;
        

        public List<int> OwnedWeapons = new List<int>();
        public List<int> OwnedThrusters = new List<int>();
        public List<int> OwnedShields = new List<int>();
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
    

    public void UpdateLevel(int level)
    {
        saveData.SavedLevel = level;
        SaveGame();
    }

    public void UpdateUpgrades(int weapon, int shield, int thruster)
    {
        saveData.SavedPlayerWeapon = weapon;
        saveData.SavedPlayerShield = shield;
        saveData.SavedPlayerThruster = thruster;

        SaveGame();
    }

    public void UpdateOwnedWeapons(int weaponID)
    {
        saveData.OwnedWeapons.Add(weaponID);

        SaveGame();
    }

    public void UpdateOwnedThrusters(int thrusterID)
    {
        saveData.OwnedThrusters.Add(thrusterID);

        SaveGame();
    }

    public void UpdateOwnedShields(int shieldID)
    {
        saveData.OwnedShields.Add(shieldID);

        SaveGame();
    }

    public void UpdateCurrency(float amount)
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
