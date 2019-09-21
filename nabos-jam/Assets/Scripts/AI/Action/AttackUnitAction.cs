using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Attack Unit")]
public class AttackUnitAction : Action
{
    public override void PerformAction() 
    {
        if (_unit)
        {
            AttackHandler ah = _unit.GetComponent<AttackHandler>();
            if (ah)
            {
                Unit target = _unit.GetComponent<AttackHandler>().targetUnit;
                if (target) ah.Attack(target);
            }
        }
    }
}
