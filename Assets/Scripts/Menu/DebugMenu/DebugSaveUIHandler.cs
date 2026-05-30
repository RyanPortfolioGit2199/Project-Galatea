using UnityEngine;
using System.IO;

public class DebugSaveUIHandler : MonoBehaviour
{
    [SerializeField] GameObject debugSaveMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void DeleteSaveData()
    {
        if(File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            File.Delete(Application.persistentDataPath + "/savefile.json");
            debugSaveMenu.SetActive(false);
        }
    }

    public void ExitDebugMenu()
    {
        debugSaveMenu.SetActive(false);
    }
}
