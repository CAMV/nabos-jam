using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class implements a finite state machine as explained
/// in the book Artificial Intelligence for Games, with a few
/// modifications. It has an extra global state similar to 
/// the animator's "any" state, that can happen in any moment
/// </summary>
[RequireComponent(typeof(Unit))]
public class FiniteStateMachine : MonoBehaviour
{
    [SerializeField]
    List<FSMState> states;              //All the states the machine uses
    [SerializeField]
    List<FSMTransition> transitions;    //All the transitions the machine uses
    [SerializeField]
    List<Action> actions;               //All the actions the machine has
    [SerializeField]
    FSMState initialState;       //The initial state
    FSMState currentState;       //The state the machine is in
    [SerializeField]
    FSMState anyState;           //A state that can be triggered from anywhere

    private void Start()
    {
        currentState = initialState;

        //Initializes all unit info actions and transitions
        Unit unit = GetComponent<Unit>();
        foreach (FSMTransition transition in transitions)
        {
            if (transition.UsesUnitInfo())
            {
                transition.Initialize(unit);
            }
        }
        foreach (Action action in actions)
        {
            if (action.usesUnitInfo)
            {
                action.Initialize(unit);
            }
        }

    }

    private FSMTransition GetValidTransition(FSMState state)
    {
        FSMTransition triggeredTransition = null;
        foreach (FSMTransition t in state.GetTransitions())
        {
            if (t.IsTriggered())
            {
                triggeredTransition = t;
                break;
            }
        }
        return triggeredTransition;
    }

    private void Update()
    {
        FSMTransition triggeredTransition = null;

        if (anyState)
        {
            triggeredTransition = GetValidTransition(anyState);
        }

        if (!triggeredTransition)
        {
            triggeredTransition = GetValidTransition(currentState);
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
