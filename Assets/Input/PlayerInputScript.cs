using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public bool debugMenu;
    public bool shoot;
    public bool dodge;
    public bool pause;

    public event Action<InputAction.CallbackContext> OnFireContextChanged;

    // Direct event for when fire button status changes
    public event Action<bool> OnFireInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        move = context.ReadValue<Vector2>();
    }

    public void OnDebugMenu(InputAction.CallbackContext context)
    {
 
        if (context.performed)
        {
            debugMenu = context.ReadValueAsButton();
            Debug.Log("Debug Button Pressed");
        }else if (context.canceled)
        {
            debugMenu = false;
        }
       
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        OnFireContextChanged?.Invoke(context);

        if(context.started || context.performed)
        {
            OnFireInput?.Invoke(true);
        }
        else if(context.canceled)
        {
            OnFireInput?.Invoke(false);
        }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        dodge = context.ReadValueAsButton();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            pause = false;
        }
        else if (context.performed)
        {
            pause = context.ReadValueAsButton();
        }
        else if (context.canceled)
        {
            pause = false;
        }
        
        
    }


}
