using UnityEngine;
using System.IO;
using System.Collections.Generic;


[System.Serializable]
    
 
    public class GameSaveData
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

    public class SettingsSaveData
    {
        public int SavedResolution;
        public int SavedFPS;
        public bool SavedVsync;
    }
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance {get; private set;}

    public GameSaveData saveData = new GameSaveData();
    public SettingsSaveData settingsSaveData = new SettingsSaveData();
    private string savePath;
    private string settingsPath;

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
        settingsPath = Path.Combine(Application.persistentDataPath, "settings.json");

        LoadGame();
        LoadSettings();
    }
    
    // Game Save Data Logic Start
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
            saveData = JsonUtility.FromJson<GameSaveData>(json);
        }
    }

    // Game Save Data Logic End


    // Settings Save Data Logic Start

    public void UpdateResolution(int resolutionID)
    {
        settingsSaveData.SavedResolution = resolutionID;
        SaveSettings();
    }

    public void UpdateMaxFPS(int fpsID)
    {
        settingsSaveData.SavedFPS = fpsID;
        SaveSettings();
    }

    public void UpdateVsyncToggle(bool toggleValue)
    {
        settingsSaveData.SavedVsync = toggleValue;
    }

    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(settingsSaveData);
        File.WriteAllText(settingsPath, json);
    }

    public void LoadSettings()
    {
        if (File.Exists(settingsPath))
        {
            string json = File.ReadAllText(settingsPath);
            settingsSaveData = JsonUtility.FromJson<SettingsSaveData>(json);
        }
    }

    // Settings Save Data Logic End
}
