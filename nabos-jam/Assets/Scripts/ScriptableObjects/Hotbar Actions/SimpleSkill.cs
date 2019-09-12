using UnityEngine;

/// <summary>
/// Class <c> SimpleSkill </c> represents a skill that does not need further input, just to be executed.
/// </summary>
[CreateAssetMenu(menuName = "Skills/Simple Skill")]
public class SimpleSkill : Skill
{
    /// <summary>
    /// Checks if the conditions for the skill to be casted are met.
    /// </summary>
    override protected bool CheckPreCondition()
    {
        return true;
    }

    /// <summary>
    /// Executes the skill
    /// </summary>
    override public void Execute(bool isQuickCast)
    {
        string qcMsg = isQuickCast ? "QuickCast " : "";
        Debug.Log(qcMsg + "Skill " + _name + " casted!");
    }


}