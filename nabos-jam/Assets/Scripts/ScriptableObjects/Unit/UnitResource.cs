using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that represents a Unit's Resource. It depends on attributes to be calculated.
/// The _baseValue represent the proportion of the maxValue of the resource the unit currently have.
/// </summary>
public class UnitResource : UnitStat
{
    [SerializeField]
    private int _maxValue;
    
    [SerializeField]
    private UnitStat _regeneration;

    public event EventHandler Depleated;
    private bool _IsRegenerating = false;

    //////////////// PROPERTIES ////////////////

    /// <summary>
    /// Gets the current value of the Resource
    /// </summary>
    override public int CurrentValue {
        get {
            return Mathf.CeilToInt(Mathf.Lerp(0, _maxValue, _baseValue)); 
        }
    }

    /// <summary>
    /// Gets the max value of the Resource
    /// </summary>
    /// <value></value>
    public int MaxValue {
        get {
            return CalculateCurrentValue(_maxValue);
        }
    }

    /// <summary>
    /// Regeneration stat that represents the regeneration rate of the resource.
    /// </summary>
    /// <value></value>
    public UnitStat Regeneration {
        get {
            return _regeneration;
        }
    }

    //////////////// METHODS ////////////////
    
    /// <summary>
    /// Send a message to all components that wnats to know if the resource is depleated.
    /// </summary>
    protected void OnDepleated()
    {
        EventHandler handler = Depleated;

        if (handler != null)
            handler(this, EventArgs.Empty);
    }

    /// <summary>
    /// Change the current stock of the resource by a precentage value.
    /// </summary>
    /// <param name="change">Percentage to add/take based on MaxValue.</param>
    public void ChangePerecentage(float change)
    {
        if (_baseValue + change > 1)
            _baseValue = 1;
        else if (_baseValue + change < 0)
        {
            _baseValue = 0;
            OnDepleated();
        } 
        else
            _baseValue += change;

        OnValueChanged();
    }

    /// <summary>
    /// Change the current stock of the resource by a base value.
    /// </summary>
    /// <param name="change">Base value to add/take based on MaxValue.</param>
    public void ChangeBase(int change)
    {
        float changeToPercent = change/MaxValue;
        ChangePerecentage(changeToPercent);
    }

    /// <summary>
    /// Function thas is called when the rate of regeneration is changed.
    /// </summary>
    private void OnRateChange(object sender, EventArgs e)
    {
        if (!_regeneration)
            return;

        if (_regeneration.CurrentValue >= 0)
            return;

        if (!_IsRegenerating)
            return;

        GameManager.Instance.tickHandler += OnRegeneration;
        _IsRegenerating = true;
    }

    /// <summary>
    /// Function called when tick event to regenerate the resource based on its regeneration stat.
    /// </summary>
    private void OnRegeneration(object sender, TickEventArgs e)
    {
        ChangeBase(Mathf.RoundToInt( e.value *  _regeneration.CurrentValue));
        if (CurrentValue >= MaxValue)
        {
            GameManager.Instance.tickHandler -= OnRegeneration;
            _IsRegenerating = false;
        }
    }

    /// <summary>
    /// Function called when the value is changed.true Call all functions subscribes to the event and turn
    /// </summary>
    protected override void OnValueChanged()
    {
        base.OnValueChanged();

        if (!_regeneration)
            return;

        if (CurrentValue < MaxValue && !_IsRegenerating)
        {
            GameManager.Instance.tickHandler += OnRegeneration;
            _IsRegenerating = true;
        }
    }

    /// <summary>
    /// Initializes the Resource. Used right after instantiating the resouce scriptable object. Always starts with max resources.
    /// </summary>
    /// <param name="unit">Unit that holds the instance of the Resource.</param>
    /// <param name="baseValue">Initia max value of the resource.</param>
    public override void Initialize(Unit unit, int maxValue)
    {
        base.Initialize(unit, 1);
        _maxValue = maxValue;

        if (_regeneration && _regeneration.GetType() != typeof(UnitResource))
        {
            _regeneration = unit.Properties.GetStat(_regeneration);
            _regeneration.ValueChanged += OnRateChange;
        }
        else
            _regeneration = null;
    }
    
}
