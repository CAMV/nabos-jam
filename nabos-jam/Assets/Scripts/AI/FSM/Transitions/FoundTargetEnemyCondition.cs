using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Conditions/Has Target Enemy")]
public class FoundTargetEnemyCondition : Condition
{
    public override bool IsTriggered()
    {
        if (_unit)
        {
            UAttackComponent ac = _unit.GetComponent<UAttackComponent>();
            UPropertyComponent pc = _unit.GetComponent<UPropertyComponent>();
            if (ac && pc)
            {
                float los = pc.GetAttribute("LineOfSight").BaseValue;
                Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            }
        }
        return false;
    }
}
