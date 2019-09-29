using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to handle if the unit got a target by other means
/// e.g. the target was attacked
/// </summary>
[CreateAssetMenu(menuName = "AI/Conditions/Has Target Enemy")]
public class HasTargetEnemyCondition : Condition
{
    public override bool IsTriggered()
    {
        if (_unit)
        {
            UAttackComponent ac = _unit.GetComponent<UAttackComponent>();
            return ac && ac.Target;
        }
        return false;
    }
}
