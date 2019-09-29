using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if the unit is stopped
/// </summary>
[CreateAssetMenu(menuName = "AI/Conditions/IsStopped")]
public class IsStoppedCondition : Condition
{
    public override bool IsTriggered() {
        return (_unit.Movements && !_unit.Movements.isMoving());
    }
}
