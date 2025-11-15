using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    public void StartNew()
    {
        // SceneManager is the class that handles everything related to loading and unloading scenes
        //SceneManager.LoadScene(1); //The LoadScene parameter is a number, It is the index of the scene that you want to load. A scene's index is defined in the Build Settings window.
        Debug.Log("Start a New Game");
    }

    public void Continue()
    {
        Debug.Log("Continue if an existing Save Data Exits");
    }

    public void DebugMenu()
    {
        Debug.Log("Open Debug Level Picker");
    }

    public void Exit()
    {
        

        // # are for conditional compiling. With the # code wont be compiled and executed they are instructions for the compiler. It is used branch the code based on where the code is compiled — in the Editor, or for a build
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player. Application.Quit is a premade function to close you application(game) but only works for built games not in editor
#endif
    }
}
