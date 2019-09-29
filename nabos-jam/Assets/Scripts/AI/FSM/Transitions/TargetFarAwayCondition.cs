using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Conditions/Target Far Away")]
public class TargetFarAwayCondition : Condition
{
    private Unit target;
    UAttackComponent ac;

    public override void Initialize(Unit unit)
    {
        base.Initialize(unit);
        ac = _unit.GetComponent<UAttackComponent>();
        if (ac) 
        {
            target = ac.Target;
        }
    }

    public override bool IsTriggered()
    {
        if (_unit)
        {
            if (!target)
            {
                if (ac) 
                {
                    target = ac.Target;
                }
            }
            else
            {
                bool isFarAway = ac.CheckIfInRange();
                if (negateCondition)
                {
                    isFarAway = !isFarAway;
                }
                return isFarAway;
            }
        }
        return false;
    }
}
