using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Class that models the basic datas of a unit's property.virtual a property can be an Attribute, a Stat or a Resource.
/// </summary>
public abstract class UnitProperty : ScriptableObject
{
    // Structs to represent modifiers values with their stacks
    protected struct  AddPair
    {
        public int stacks;
        public int value;

        public AddPair(int value, int stacks)
        {
            this.value = value;
            this.stacks = stacks;
        }
    }

    protected struct  ScalePair
    {
        public int stacks;
        public float value;

        public ScalePair(float value, int stacks)
        {
            this.value = value;
            this.stacks = stacks;
        }

    }

    [SerializeField]
    protected GUIData _guiData;


    // Value of the property before any calculations
    [SerializeField]
    protected float _baseValue = 0;
    protected Unit _originUnit = null;

    // Easy access to modifiers value
    protected Dictionary<string, ScalePair> _toScale = new Dictionary<string, ScalePair>();
    protected Dictionary<string, AddPair> _toAdd = new Dictionary<string, AddPair>();

    // Listener to update all stats dependend on this attribute
    public event EventHandler ValueChanged;

    //////////////// PROPERTIES ////////////////

    /// <summary>
    /// Gets the GUI data for the Property.
    /// </summary>
    public GUIData GUIData {
        get {
            return _guiData;
        } 
    }

    /// <summary>
    /// Gets the current value of the stats
    /// </summary>
    public virtual int CurrentValue {
        get {
            return CalculateCurrentValue(_baseValue);
        }
    }

    //////////////// METHODS ////////////////

    /// <summary>
    /// Initialize the property with a given unit and a initial base value.
    /// </summary>
    /// <param name="unit">Unit of the property.</param>
    /// <param name="baseValue">Initial base value.</param>
    public virtual void Initialize(Unit unit, int baseValue)
    {
        _originUnit = unit;
        _baseValue = baseValue;
        _toAdd = new Dictionary<string, AddPair>();
        _toScale = new Dictionary<string, ScalePair>();
    }

    /// <summary>
    /// Calculates the current value of the property.
    /// </summary>
    /// <param name="baseValue">Value to use as base value.</param>
    /// <returns>Value after adding all modifiers of the property.</returns>
    protected virtual int CalculateCurrentValue(float baseValue)
    {
        float totalScale = 1;
        int totalAdd = 0;

        foreach (var scale in _toScale)
            totalScale *= scale.Value.value * scale.Value.stacks;

        foreach (var add in _toAdd)
            totalAdd += add.Value.value * add.Value.stacks;

        return Mathf.RoundToInt(baseValue * totalScale) + totalAdd;
    }

    /// <summary>
    /// Send an update message to all components that relies on this property.
    /// </summary>
    protected virtual void OnValueChanged()
    {
        EventHandler handler = ValueChanged;

        if (handler != null)
            handler(this, EventArgs.Empty);

    }

    /// <summary>
    /// Add a new multiplier modifier to the calculations of the property
    /// </summary>
    /// <param name="label">Label to identify the modifier.</param>
    /// <param name="value">Value of the modifier to use to multiply the property.</param>
    public void AddMultiplier(string label, float value, int stacks = 1)
    {
        if (stacks < 1)
            return;
        
        if (value < 0)
            return;    
        
        // If the modifier is not active, add it. If it is, add stacks to it.
        if (_toScale.ContainsKey(label))
        {
            _toScale[label] = new ScalePair(_toScale[label].value, _toScale[label].stacks + stacks);
            OnValueChanged();
        }
        else 
            _toScale.Add(label, new ScalePair(value, stacks));

    }
    
    /// <summary>
    /// Add a new additive modifier to the calculations of the property
    /// </summary>
    /// <param name="label">Label to identify the modifier.</param>
    /// <param name="value">Value of the modifier to use to add to the property.</param>
    public void AddAdditive(string label, int value, int stacks = 1)
    {
        if (stacks < 1)
            return;

        if (_toAdd.ContainsKey(label))
        {
            _toAdd[label] = new AddPair(_toAdd[label].value, _toAdd[label].stacks + stacks);
            OnValueChanged();
        }
        else
            _toAdd.Add(label, new AddPair(value, stacks)); 
    }

    /// <summary>
    /// Removes a multiplier with a given label.
    /// </summary>
    /// <param name="label">Label of the multiplier to remove.</param>
    public void RemoveMultiplier(string label, int stacks = 1)
    {
        if (_toScale.ContainsKey(label))
        {
            if (_toScale[label].stacks - stacks <= 0)
                _toScale.Remove(label);
            else
                _toScale[label] = new ScalePair(_toScale[label].value, _toScale[label].stacks - stacks);

            OnValueChanged();
        }
    }
    
    /// <summary>
    /// Removes an additive with a given label.
    /// </summary>
    /// <param name="label">Label of the additive to remove.</param>
    public void RemoveAdditive(string label, int stacks = 1)
    {
        if (_toAdd.ContainsKey(label))
        {
            if (_toAdd[label].stacks - stacks <= 0)
                _toAdd.Remove(label);
            else
                _toAdd[label] = new AddPair(_toAdd[label].value, _toAdd[label].stacks - stacks);
                
            OnValueChanged();
        }
    }

    /// <summary>
    /// Clones a given instance of the same skill values into the skill.
    /// </summary>
    /// <param name="property">Property to clone from.virtual Has to be the same kind of property.</param>
    public virtual void Clone(UnitProperty property)
    {
        if (this.GetType() != property.GetType())
            return;

        if (this._guiData.Name != property._guiData.Name)
            return;
        
        this._baseValue = property._baseValue;
        OnValueChanged();
    }
}