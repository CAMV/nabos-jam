using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Move Toward")]
public class MoveTowardsTargetAction : Action
{

    public override void PerformAction() {
        if (_unit)
        {
            UAttackComponent ac = _unit.GetComponent<UAttackComponent>();
            if (ac)
            {
                Unit target = ac.Target;
                if (target && _unit.Movements) _unit.Movements.MoveWithinRange();
            }
        }
    }
}
