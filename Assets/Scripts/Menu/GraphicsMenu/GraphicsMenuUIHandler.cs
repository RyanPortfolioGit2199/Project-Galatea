using UnityEngine;
using TMPro;

public class GraphicsMenuUIHandler : MonoBehaviour
{
    [SerializeField] TMP_Dropdown maxFrameRateDropdown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ResolutionDropdown(int index)
    {
        switch (index)
        {
            case 0:
            Debug.Log("Setting Resolution to 1280x720");
            GraphicsManager.Instance.SetResolution(0);
            break;

            case 1:
            Debug.Log("Setting Resolution to 1920x1080");
            GraphicsManager.Instance.SetResolution(1);
            break;

            case 2:
            Debug.Log("Setting Resolution to 2560x1440");
            GraphicsManager.Instance.SetResolution(2);
            break;
            
        }
    }

    public void VSyncToggle(bool toggleValue)
    {
        if (toggleValue)
        {
            maxFrameRateDropdown.interactable = false;
        }
        else if (!toggleValue)
        {
            maxFrameRateDropdown.interactable = true;
        }

        GraphicsManager.Instance.ToggleVerticalSync(toggleValue);

    }

    public void MaxFPSDropdown(int index)
    {
        switch (index)
        {
            case 0:
            Debug.Log("Setting Max FPS to Unlimited");
            GraphicsManager.Instance.MaxFPSController(0);
            break;

            case 1:
            Debug.Log("Setting Max FPS to 30");
            GraphicsManager.Instance.MaxFPSController(1);
            break;

            case 2:
            Debug.Log("Setting Max FPS to 60");
            GraphicsManager.Instance.MaxFPSController(2);
            break;

            case 3:
            Debug.Log("Setting Max FPS to 90");
            GraphicsManager.Instance.MaxFPSController(3);
            break;

            case 4:
            Debug.Log("Setting Max FPS to Unlimited");
            GraphicsManager.Instance.MaxFPSController(4);
            break;

        }
    }

    public void ExitButton()
    {
        GraphicsManager.Instance.GraphicsMenuToggle(false);

    }
}
