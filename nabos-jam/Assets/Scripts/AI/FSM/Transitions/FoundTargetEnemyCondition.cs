using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Actively searches for enemies within a given range (i.e. a line of sight property)
/// </summary>
[CreateAssetMenu(menuName = "AI/Conditions/Found Target Enemy")]
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
                float los = pc.GetAttribute("Line Of Sight").BaseValue;
                Collider[] hitColliders = Physics.OverlapSphere(_unit.transform.position, los);
                foreach (Collider col in hitColliders)
                {
                    Unit u = col.GetComponent<Unit>();
                    if (u && u.tag != _unit.tag)
                    {
                        ac.SetAttackTarget(u);
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
