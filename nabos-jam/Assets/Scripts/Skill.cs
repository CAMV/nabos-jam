using UnityEngine;

/// <summary>
/// Class <c>Skill</c> models the basic structure to be used to represent the different skills in the game.
/// </summary>
public abstract class Skill : ScriptableObject
{
    protected Unit _originUnit;

    protected bool _isReady;

    /// <summary>
    /// Initialize the skill with the unit caster.
    /// </summary>
    /// <param name="oUnit">Unit that cast the skill</param>
    protected void Initialize(Unit oUnit)
    {
        this._originUnit = oUnit;
        this._isReady = false;
    }

    /// <summary>
    /// Checks if the conditions for the skill to be casted are met.
    /// </summary>
    public abstract bool CheckPreCondition();

    /// <summary>
    /// Casts the skill
    /// </summary>
    public abstract void Cast();

}