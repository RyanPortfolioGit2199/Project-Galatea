using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public Stack<State> States {get; set;}

    void Awake()
    {
        States = new Stack<State>();
    }

    private void Update()
    {

        /*
        if the Get GetCurrentState() doesnt return null,
        Get that States ActiveAction and Invoke it.
        */
        if (GetCurrentState() != null)
        {
            GetCurrentState().ActiveAction.Invoke();
        }
    }


    /*
        For the PushState method it needs to be public because it will be called outside of this script.

        This Method will take a State and Put it at the top of the States list

        If there is a State Active at the top of the list use OnExit to exit thate method

        then Create the New State that is needed and pass in the active, onEnter, onExit Actions into that new state

        then using Push() to push that new state onto the top of the States list to make it the Active State.
        
    */
    public void PushState(System.Action active, System.Action onEnter, System.Action onExit)
    {
        if(GetCurrentState() != null)
            GetCurrentState().OnExit();

        State state = new State(active, onEnter, onExit);
        States.Push(state);

        GetCurrentState().OnEnter();
    }


    /*
        This method PopState() will pop the State at the top of the States list(the active state)
        making the one in the state in the second position and put at the top of the States list(the active state)
    */

    public void PopState()
    {
        if(GetCurrentState() != null)
        {
            GetCurrentState().OnExit();
            GetCurrentState().ActiveAction = null;
            States.Pop();
            GetCurrentState().OnEnter();
        }
            
    }

    private State GetCurrentState()
    {
        /*
        this will check if the current number of States is greater than 0, Get the Very first State using, Peek().
        */
        return States.Count > 0 ? States.Peek() : null;
    }

}
