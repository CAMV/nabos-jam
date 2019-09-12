using UnityEngine;
using System.Collections;

/// <summary>
/// Class <c>Skill</c> models the basic structure to be used to represent the different skills in the game.
/// </summary>
public abstract class Skill : ScriptableObject, IHotbarAction
{
    [SerializeField]
    protected string _name = "";

    [SerializeField]
    protected Sprite _icon = null;

    [SerializeField]
    protected float _baseCoolDownTime = 0;

    protected Unit _originUnit;
    protected bool _isReady;
    protected float _currentCoolDown;


    /// <summary>
    /// Get the name of the Skill.
    /// </summary>
    /// <value></value>
    public string Name {
        get {
            return _name;
        } 
    }

    /// <summary>
    /// Get the icon of the Skill for GUI purpposes
    /// </summary>
    public Sprite Icon {
        get {
            return _icon;
        }
    }

    /// <summary>
    /// Gets the current cool down of the skill. Where 0, the skill is ready to be executed and 1 the skill has just being executed.
    /// </summary>
    public float CurrentCooldown {
        get {
            return _currentCoolDown/_baseCoolDownTime;
        }
    }

    /// <summary>
    /// Gets the cooldown of the skill.
    /// </summary>
    /// <returns></returns>
    public bool isActivable {
        get {
            return (_currentCoolDown == 0) && CheckPreCondition();
        }
    }

    /// <summary>
    /// Initialize the skill with the unit caster.
    /// </summary>
    /// <param name="oUnit">Unit that cast the skill</param>
    public void Initialize(Unit oUnit)
    {
        this._originUnit = oUnit;
        this._isReady = false;
    }

    /// <summary>
    /// Checks if the conditions for the skill to be casted are met.
    /// </summary>
    protected abstract bool CheckPreCondition();

    /// <summary>
    /// Executes the skill
    /// </summary>
    public virtual void Execute(bool isQuickCast){}


    public virtual bool Compare(IHotbarAction action) 
    {
        if (this.GetType() != action.GetType())
            return false;

        if (this.Name != action.Name)
            return false;

        if (this._originUnit != ((Skill)action)._originUnit)
            return false;

        return true;
    }
    /// <summary>
    /// Cooldown timer for the skill
    /// </summary>
    public IEnumerator CoolDownTimer()
    {
        while (_currentCoolDown > 0) 
        {
            yield return new WaitForEndOfFrame();
            _currentCoolDown -= Time.deltaTime;
        }

        _currentCoolDown = 0;
    }


}