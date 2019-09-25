using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class <c> SimpleSkill </c> represents a skill that does not need further input, just to be executed.
/// </summary>
[CreateAssetMenu(menuName = "Skills/Simple Skill")]
public class BasicSkill : Skill
{

    public enum SkillTarget {
        Myself,
        Allies,
        Enemies, 
        AllButMe
    }

    [SerializeField]
    private bool _isHardCondition;

    [SerializeField]
    protected SkillTarget _targetType;

    [SerializeField]
    protected int _numTargets;

    protected List<Unit> _targetUnits;

    //////////////// METHODS ////////////////

    /// <summary>
    /// Checks if the conditions for the skill to be casted are met, and gets the target units.
    /// </summary>
    override protected bool CheckPreCondition()
    {
        _targetUnits = new List<Unit>();

        if (!base.CheckPreCondition())
            return false;

        // Check if valid targets
        if (_targetType != SkillTarget.Myself )
        {
            if (_isHardCondition)
            {
                Collider[] collidersInRange = Physics.OverlapSphere(_originUnit.transform.position, _range);

                foreach (var Collider in collidersInRange)
                {
                    Unit currentUnit = Collider.GetComponentInParent<Unit>(); 
                    if (currentUnit)
                    {
                        if (_targetType == SkillTarget.Allies && currentUnit.tag == "GoodNpc")
                            _targetUnits.Add(currentUnit);
                        else if (_targetType == SkillTarget.Enemies && currentUnit.tag == "BadNpc")
                            _targetUnits.Add(currentUnit);
                        else if (_targetType == SkillTarget.AllButMe)
                            _targetUnits.Add(currentUnit);
                    }
                }

                if (_targetUnits.Count < _numTargets)
                    return false;
            }
        }
        else
            _targetUnits.Add(_originUnit);            

        return true;
    }

    /// <summary>
    /// Executes the skill
    /// </summary>
    override public void Execute(bool isQuickCast)
    {
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
            foreach (var Unit in _targetUnits)
            {
                InitializeActionObjects(i, Unit.Animation[UnitPart.Base].position);
            }
        }
    }


}