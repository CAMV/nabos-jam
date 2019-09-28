﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Attack Unit")]
public class AttackUnitAction : Action
{
    public override void PerformAction() 
    {
        if (_unit)
        {
            UAttackComponent ac = _unit.GetComponent<UAttackComponent>();
            // AttackHandler ah = _unit.GetComponent<AttackHandler>();
            if (ac)
            {
                Unit target = ac.Target;
                if (target) ac.Attack(target);
            }
        }
    }
}
