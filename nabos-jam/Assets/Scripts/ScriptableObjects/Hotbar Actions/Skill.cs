using UnityEngine;
using System.Collections;

/// <summary>
/// Class <c>Skill</c> models the basic structure to be used to represent the different skills in the game.
/// </summary>
public abstract class Skill : ScriptableObject, IHotbarAction
{
    [SerializeField]
    protected GUIData _guiData;

    [SerializeField]
    protected ActionObject[] _actionObjects = new ActionObject[0];

    [SerializeField]
    protected UnitPart[] _spawnPoints;

    [SerializeField]
    protected float _baseCoolDownTime = 0;

    [SerializeField]
    private VfxAction[] _castEffects;

    [SerializeField]
    private UnitResource[] _resourceCost;

    [SerializeField]
    private int[] _valueCost;
    
    [Range(0, 10)]
    [SerializeField]
    protected int _range;

    protected Unit _originUnit;
    protected bool _isReady;
    protected float _currentCoolDown;

    //////////////// PROPERTIES ////////////////

    /// <summary>
    /// Get the GUIData of the Skill.
    /// </summary>
    public GUIData GUIData {
        get {
            return _guiData;
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
    /// Maximun range allowed for the skill
    /// </summary>
    /// <value></value>
    public int Range {
        get {
            return _range;
        }
    }

    //////////////// METHODS ////////////////

    /// <summary>
    /// Initialize the skill with the unit caster.
    /// </summary>
    /// <param name="oUnit">Unit that cast the skill</param>
    public void Initialize(Unit oUnit)
    {
        this._originUnit = oUnit;

        for (int i = 0; i < _actionObjects.Length; i++)
        {
            _actionObjects[i]  =ScriptableObject.Instantiate(_actionObjects[i]);
            _actionObjects[i].Initialice(oUnit);
        }

        this._isReady = false;
    }

    /// <summary>
    /// Checks if the conditions for the skill to be casted are met.
    /// </summary>
    protected virtual bool CheckPreCondition(){
        
        if (_currentCoolDown > 0)
            return false;


        // Check the cost in resources of the skill
        for (int i = 0; i < _resourceCost.Length; i++)
        {
            UnitResource currentResource = _originUnit.Properties.GetResource(_resourceCost[i]);
            if (currentResource)
            {
                if (currentResource.CurrentValue > _valueCost[i])
                    return false;
            }
            else
                return false;

        }

        return true;
    }

    /// <summary>
    /// Executes the skill
    /// </summary>
    public virtual void Execute(bool isQuickCast)
    {
        if (!CheckPreCondition())
            return;
            
        string qcMsg = isQuickCast ? "quick-casted " : "casted";
        Debug.Log(_originUnit.name + " " + qcMsg  + _guiData.Name + "!");
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

    /// <summary>
    /// Remove the amount of resources neede to cast the spell. 
    /// </summary>
    protected void PayThCost()
    {
        for (int i = 0; i < _resourceCost.Length; i++)
        {
            _originUnit.Properties.GetResource(_resourceCost[i]).ChangeBase(_valueCost[i]);
        }
    }

    /// <summary>
    /// Creates the actions objects and Vfx Associated with the skill.
    /// </summary>
    public virtual void Cast()
    {
        PayThCost();
    }

    /// <summary>
    /// Initialize all action objects associated with the the skills
    /// </summary>
    /// <param name="actionIndex">Index of the action in the action list of the skill.</param>
    /// <param name="spawnPosition">Position in world coord where to spawn the object.</param>
    protected void InitializeActionObjects(int actionIndex, Vector3 spawnPosition)
    {
        ActionObject currentAction;

        if ( _actionObjects[actionIndex].GetType() == typeof(AreaAction))
            currentAction = GameObject.Instantiate(_actionObjects[actionIndex], spawnPosition, Quaternion.identity);    
        else
            currentAction = GameObject.Instantiate(_actionObjects[actionIndex], _originUnit.Animation[_spawnPoints[actionIndex]]);

        currentAction.Initialice(_originUnit);

        if ( _actionObjects[actionIndex].GetType() == typeof(ProjectileAction))
            ((ProjectileAction) currentAction).Launch(spawnPosition);
    }


}