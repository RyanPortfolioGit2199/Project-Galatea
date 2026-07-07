using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonGridManager : MonoBehaviour
{
    [SerializeField] Button[] gunUpgradeButtons;
    [SerializeField] Button[] shieldUpgradeButtons;
    [SerializeField] Button[] thrusterUpgradeButtons;

    [SerializeField] GameObject lastSelectedButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject currentSelection = EventSystem.current.currentSelectedGameObject;

        if (currentSelection != lastSelectedButton)
        {
            Debug.Log("Selected Button: " + currentSelection.name);
        }

        //Delete later if getting the ActiveWeapons weaponsSO works for checking which upgrade the player selected.
    }
}
