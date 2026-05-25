using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance {get; private set;}

    [Header("Save Data")]
    public int SavedLevel;
    public int SavedCurrency;
    public int SavedPlayerWeapon;
    public int SavedPlayerShield;
    public int SavedPlayerThruster;

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

        LoadCurrency();
    }
    
    [System.Serializable]
    class SaveData
    {
        public int SavedLevel;
        public int SavedCurrency;
        public int SavedPlayerWeapon;
        public int SavedPlayerShield;
        public int SavedPlayerThruster;
    }

    public void SaveLevel()
    {
        
    }

    public void LoadLevel()
    {
        
    }

    public void SaveCurrency(int currentCurrency)
    {
        SaveData data = new SaveData();
        data.SavedCurrency = currentCurrency;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadCurrency()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            SavedCurrency = data.SavedCurrency;
        }
    }
}
