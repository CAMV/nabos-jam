using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Move Toward")]
public class MoveTowardsTargetAction : Action
{

    public override void PerformAction() {
        if (_unit)
        {
            AttackHandler ah = _unit.GetComponent<AttackHandler>();
            if (ah)
            {
                Unit target = _unit.GetComponent<AttackHandler>().targetUnit;
                if (target && _unit.Movement) _unit.Movement.Move(target.transform.position);
            }
        }
    }
}
