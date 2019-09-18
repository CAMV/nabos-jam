using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/FSM/State")]
public class FSMState : ScriptableObject
{
    [SerializeField]
    private List<FSMTransition> transitions;

    [SerializeField]
    private ActionList entryActions;
    [SerializeField]
    private ActionList currentStateActions;
    [SerializeField]
    private ActionList exitActions;

    public List<Action> GetEntryActions() {
        return entryActions.GetActions();
    }
    public List<Action> GetCurrentStateActions() {
        return currentStateActions.GetActions();
    }
    public List<Action> GetExitActions() {
        return exitActions.GetActions();
    }

    public List<FSMTransition> GetTransitions() {return transitions;}
}
