using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/FSM/ActionList")]
public class ActionList : ScriptableObject
{
    [SerializeField]
    List<Action> actions;

    public List<Action> GetActions() {
        return actions;
    }
}
