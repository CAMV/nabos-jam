using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
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

            Cast();
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