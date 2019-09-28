using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Skill that has as a traget a given unit
/// </summary>
[CreateAssetMenu(menuName = "Skills/UnitSkill")]
public class Unitskill : BasicSkill
{
    protected Unit _unitInput;

    /// <summary>
    /// Exceute function for hotbar action
    /// </summary>
    override public void Execute(bool isQuickCast)
    {
        base.Execute(isQuickCast);

        if (isQuickCast)
        {
            // Get raycast point
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
                return;

            _unitInput = hit.collider.gameObject.GetComponentInParent<Unit>();

            if (!_unitInput)
                return;

            Cast(_unitInput);
        }
        else
        {
            _originUnit.Skills.GetSkillInput(this, VectorSkillType.Unit);      
        }
    }

    /// <summary>
    /// Cast the skill
    /// </summary>
    public void Cast(Unit target)
    {
        _targetUnits = new List<Unit>();
        _targetUnits.Add(target);

        Cast();
    }
}