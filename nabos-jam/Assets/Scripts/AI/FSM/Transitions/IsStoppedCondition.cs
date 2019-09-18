using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Conditions/IsStopped")]
public class IsStoppedCondition : Condition
{
    [SerializeField]
    Unit unit;
    public override bool IsTriggered() {
        return (unit.Movement && !unit.Movement.isMoving());
    }
}
