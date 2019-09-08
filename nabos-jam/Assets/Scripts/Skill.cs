using UnityEngine;
public abstract class Skill : ScriptableObject
{
    protected Unit _originUnit;

    protected bool _isReady;

    protected Skill(Unit oUnit)
    {
        this._originUnit = oUnit;
        this._isReady = false;
    }

    public abstract bool CheckPreCondition();

    public abstract void GetRequirments();

    public abstract void Cast();

}