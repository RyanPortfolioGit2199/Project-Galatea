using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DebugManager : MonoBehaviour
{

    [SerializeField] bool debugEnabled = false;

    InputAction debugAction;
    string debugMenu = "DebugMenu";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        debugAction = InputSystem.actions.FindAction(debugMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (debugAction.WasReleasedThisFrame() && !debugEnabled)
        {
            Debug.Log("Open Debug Menu");
            debugEnabled = true;
        }
        else if (debugAction.WasReleasedThisFrame() && debugEnabled)
        {
            Debug.Log("Close Debug Menu");
        }
    }
}
