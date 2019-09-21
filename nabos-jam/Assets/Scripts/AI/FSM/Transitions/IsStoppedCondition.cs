using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Conditions/IsStopped")]
public class IsStoppedCondition : Condition
{
    public override bool IsTriggered() {
        return (_unit.Movement && !_unit.Movement.isMoving());
    }
}
