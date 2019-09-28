using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Class that represents a modifier that can affect a unit.
/// </summary>
[CreateAssetMenu(menuName = "Modifier")] 
public class Modifier : ScriptableObject
{
    [SerializeField]
    private GUIData _guiData = null;

    [SerializeField]
    protected ModifierStackComponent _stacks = new ModifierStackComponent();

    [SerializeField]
    protected ModifierTimerComponent _timer = new ModifierTimerComponent();

    [SerializeField]
    private List<PropertyModifier> _propertyModifiers = new List<PropertyModifier>();

    [SerializeField]
    private VfxAction _vfxHighlight = null;

    [SerializeField]
    private bool _isHidden = false;

    private Unit _originUnit = null;

    
    //////////////// PROPERTIES ////////////////

    /// <summary>
    /// Returns the GUI related data of the modifier. 
    /// </summary>
    public GUIData GUIData {
        get {
            if (_isHidden)
                return null;
            else
                return _guiData;
        }
    }

    /// <summary>
    /// Returns the number of the stacks the modifier currently has.
    /// </summary>
    public int Stacks {
        get {
            return _stacks.Value;
        }
    }

    //////////////// METHODS ////////////////

    /// <summary>
    /// Initializes the modifier with the unit is affecting, a tick handler, and the number of initial stacks for the modifier.
    /// </summary>
    /// <param name="unit">Unit affected by the modifier</param>
    /// <param name="stacks">Stacks to initialize the modifier</param>
    public void Initialize(Unit unit,  int stacks = 1)
    {
        if (!unit)
            return;

        _originUnit = unit;

        // Init Modifier components
        Func<int, bool> OnStackChanged = this.OnStackedChanged;
        Func<bool> OnTimerExpired = this.OnTimerExpired;

        _stacks.Initialize(OnStackChanged);
        _timer.Inititalize(OnTimerExpired);

        // Init each effect
        foreach (var mod in _propertyModifiers)
        {
            mod.Initialize(unit);
        }

        // Add the intial stacks
        _stacks.AddStack(stacks);
        _vfxHighlight.Show(unit);
    }

    /// <summary>
    /// Function that changes the current stacks by a given value.
    /// </summary>
    /// <param name="deltaStacks">amount of stack to be add/removed to the modifier.</param>
    public void ChangeStacks(int deltaStacks)
    {
        if (deltaStacks > 1)
            _stacks.AddStack(deltaStacks);
        else
            _stacks.RemoveStack(-deltaStacks);
    }

    /// <summary>
    /// Behaviour to execute when the stacks are changed. If the stacks are depleates, destroy the instance of the modifier. Updates all references to properties.
    /// </summary>
    /// <param name="deltaStack">Change on the stacks</param>
    private bool OnStackedChanged(int deltaStack)
    {
        // Update quick reference of the modifier on the properties
        foreach (var mod in _propertyModifiers)
        {
            mod.UpdatePropertyStacks(deltaStack);
        }

        // If all stacks depleated, kill the modifier
        if (_stacks.Value == 0)
        {
            _vfxHighlight.Hide();
            _originUnit.Modifiers.RemoveModifier(this);
        }

        return true;
    }

    /// <summary>
    /// Behaviur to execute when the timer of the modifier expires
    /// </summary>
    private bool OnTimerExpired()
    {
        if (!_stacks.AreStacksRemovedAtOnce && _stacks.Value > 1)
            _timer.StartTimer();

        _stacks.RemoveStack();
        return true;
    }

    


}