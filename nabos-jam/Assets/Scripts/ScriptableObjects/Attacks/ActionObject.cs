using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class that represent an attack as a game oject that collides againts targets.
/// </summary>
public abstract class ActionObject : MonoBehaviour 
{
    [SerializeField]
    protected int _baseDamage = 0;

    [SerializeField]
    protected AttackDamage[] _damages = new AttackDamage[0]; // types of damages dealt by this attack

    [SerializeField]
    private int[] _damageContibution = new int[0]; // Percentage of the base damage is dealt in each type of damage

    [SerializeField]
    private List<Modifier> _modifiers = new List<Modifier>();

    [SerializeField]
    protected AttackTriggerType _triggerType = AttackTriggerType.All;
  
    [SerializeField]
    protected Collider _myCollider = null;
   
    [SerializeField]
    protected float _duration = 0;

    [SerializeField]
    protected bool _isMultiHitAllowed = false;

    [SerializeField]
    private VfxAction _impactEffect = null;

    protected float _currentDuration = 0;
    protected Unit _parentUnit = null;
    protected Collider _parentCollider = null;
    protected bool _isInit = false;
    protected List<Unit> _hittedUnits = new List<Unit>();
    private Unit _targetUnit = null;


    
    //////////////// METHODS ////////////////

    /// <summary>
    /// Initialize the ActioObject
    /// </summary>
    /// <param name="unit">Unit that creates the action</param>
    public virtual void Initialice(Unit unit)
    {
        _parentUnit = unit;
        _hittedUnits = new List<Unit>();

        for (int i = 0; i < _damages.Length; i++)
        {
            _damages[i] = ScriptableObject.Instantiate(_damages[i]);
            _damages[i].Initialize(unit, _baseDamage * _damageContibution[i]);
        }

        if (_duration > 0)
        {
            _currentDuration = _duration;
            GameManager.Instance.tickHandler += UpdateTick;
        }

        _targetUnit = null;
        _isInit = true;
    }

    /// <summary>
    /// Initialize the ActioObject with a target unit, so the attack only affects that targeted unit
    /// </summary>
    /// <param name="unit">Unit that creates the action</param>
    /// <param name="target">Unit to target.</param>
    public virtual void Initialice(Unit unit, Unit target)
    {
        Initialice(unit);
        _targetUnit = target; 
    }

    /// <summary>
    /// Checks if the given collider is valid one to trigger.
    /// </summary>
    /// <param name="collider">Collider to check</param>
    /// <returns>True if the attack should trigger, false otherwise.</returns>
    protected bool IsValidCollider(Collider collider)
    { 
        if (collider.gameObject.layer == InputAction.TERRAIN_LAYER)
            return false;
        
        bool isTrigger = false;

        switch (_triggerType)
        {
            case AttackTriggerType.All:
                isTrigger = true;
            break;
            case AttackTriggerType.EvilNpc:
                isTrigger = collider.tag == "EvilNpc";
            break;
            case AttackTriggerType.GoodNpc:
                isTrigger = collider.tag == "GoodNpc";
            break;
            case AttackTriggerType.OtherGroup:
                isTrigger = collider.tag != gameObject.tag;
            break;
        }

        return isTrigger;
    }

    /// <summary>
    /// Adds the modifier associted with the attack to a traget Unit.
    /// </summary>
    /// <param name="targetUnit">Unit to add modifiers</param>
    protected void SendModifiers(Unit targetUnit)
    {
        if (!targetUnit)
            return;

        if (!targetUnit.Modifiers || !targetUnit.Properties)
            return;

        foreach (var mod in _modifiers)
        {
            targetUnit.Modifiers.AddModifier(mod);
        }
    }

    /// <summary>
    /// Action to be executed when the attack object makes contact to a valid target.
    /// </summary>
    protected virtual void ExecuteAction(Unit targetUnit)
    {
        SendModifiers(targetUnit);

        for (int i = 0; i < _damages.Length; i++)
        {
            _damages[i].ApplyDamage(targetUnit);
        }

    }

    /// <summary>
    /// Adds a modifier to the attack object.
    /// </summary>
    /// <param name="modifier">Modifier to add</param>
    protected void AddModifier(Modifier modifier)
    {
        if (_modifiers.Contains(modifier))
            return;

        _modifiers.Add(modifier);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        // Check if objects is initialzed
        if (!_isInit)
            return;

        if (!IsValidCollider(other))
            return;

        Unit targetUnit = other.gameObject.GetComponentInParent<Unit>();

        if(!targetUnit)
            return;
        
        if (_targetUnit && targetUnit != _targetUnit)
            return;

        if (!_isMultiHitAllowed)
        {
            if (_hittedUnits.Contains(targetUnit))
                return;

            _hittedUnits.Add(targetUnit);
        }
        
        if (_impactEffect != null)
            _impactEffect.Show(targetUnit);

        ExecuteAction(targetUnit);
    }

    /// <summary>
    /// Tick that updates the lifespan of the area and apply the effects.`
    /// </summary>
    protected virtual void UpdateTick(object sender, TickEventArgs e)
    {
        _currentDuration = _currentDuration - e.value < 0 ? 0 : _currentDuration - e.value;

        if (_currentDuration == 0)
            Destroy(this);
    }
}