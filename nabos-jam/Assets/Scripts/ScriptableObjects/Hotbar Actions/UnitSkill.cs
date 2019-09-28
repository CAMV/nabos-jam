using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Skill that has as a traget a given unit
/// </summary>
[CreateAssetMenu(menuName = "Skills/UnitSkill")]
public class Unitskill : Skill
{
    protected Unit _unitInput;
    protected Unit _targetUnit;

    //////////////// METHODS ////////////////
    
    /// <summary>
    /// Checks if the conditions for the skill to be casted are met, and gets the target units.
    /// </summary>
    override protected bool CheckPreCondition()
    {
        if (!base.CheckPreCondition())
            return false;

        if (!_targetUnit)
            return false;

        Collider[] collidersInRange = Physics.OverlapSphere(_originUnit.transform.position, _range);

        foreach (var Collider in collidersInRange)
        {
            Unit currentUnit = Collider.GetComponentInParent<Unit>(); 
            if (currentUnit)
            {
                if (currentUnit == _targetUnit)
                {
                    return true;
                }
            }
        }
          
        return false;
    }

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
        _targetUnit = target;

        if (CheckPreCondition())
            Cast();
    }

     /// <summary>
    /// Cast the skill
    /// </summary>
    override public void Cast()
    {
        base.Cast();

        for (int i = 0; i < _actionObjects.Length; i++)
        {
                InitializeActionObjects(i, _targetUnit.Animation[UnitPart.Base].position);
        }
    }
}