using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "AI/FSM/Transition")]
public class FSMTransition : ScriptableObject
{
    [SerializeField]
    private FSMState targetState;
    [SerializeField]
    private ActionList actions;
    [SerializeField]
    private Condition condition;

    public bool IsTriggered() {return condition.IsTriggered();} 
    public FSMState GetTargetState() {return targetState;}
    public List<Action> GetAction() {return actions.GetActions();}
}
