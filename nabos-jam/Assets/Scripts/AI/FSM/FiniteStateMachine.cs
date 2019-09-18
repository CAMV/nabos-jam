using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class implements a finite state machine as explained
/// in the book Artificial Intelligence for Games, with a few
/// modifications. It has an extra global state similar to 
/// the animator's "any" state, that can happen in any moment
/// </summary>
public class FiniteStateMachine : MonoBehaviour
{
    [SerializeField]
    FSMState initialState;       //The initial state
    FSMState currentState;       //The state the machine is in
    [SerializeField]
    FSMState anyState;           //A state that can be triggered from anywhere

    private void Start()
    {
        currentState = initialState;
    }

    private void Update()
    {
        FSMTransition triggeredTransition = null;

        foreach (FSMTransition t in currentState.GetTransitions())
        {
            if (t.IsTriggered())
            {
                triggeredTransition = t;
                break;
            }
        }

        List<Action> actions = new List<Action>();
        if (triggeredTransition)
        {
            FSMState targetState = triggeredTransition.GetTargetState();

            actions.AddRange(currentState.GetExitActions());
            actions.AddRange(triggeredTransition.GetAction());
            actions.AddRange(targetState.GetEntryActions());

            currentState = targetState;
        }
        else
        {
            actions.AddRange(currentState.GetCurrentStateActions());
        }

        //run actions  
        //TODO move this to an action handler        
        foreach(Action action in actions) 
        {
            action.PerformAction();
        }
    }
}
