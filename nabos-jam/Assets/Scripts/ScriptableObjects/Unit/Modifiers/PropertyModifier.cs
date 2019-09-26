using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class that models the specific modifier of a property
/// </summary>
[Serializable]
public class PropertyModifier {

    [SerializeField]
    protected UnitProperty _property;

    // Effect values
    [SerializeField]
    protected ModValueType _modType = ModValueType.Additive; //Additive or Multiplicative

    [SerializeField]
    protected int _baseValue = 0;

    //////////////// METHODS ////////////////

    /// <summary>
    /// Initlize the effect of a modifier, giving a unit search the property from.
    /// </summary>
    /// <param name="u"></param>
    public virtual void Initialize(Unit u)
    {
        if (_property.GetType() == typeof(UnitStat))
        {
            _property = u.Properties.GetStat((UnitStat) _property);
        }
        else if (_property.GetType() == typeof(UnitAttribute))
        {
            _property = u.Properties.GetAttribute((UnitAttribute) _property);
        }
        else 
        {
            _property = u.Properties.GetResource((UnitResource) _property);
        }
    }

    /// <summary>
    /// Updates the reference of the modifier to the property modified with a given stack change.
    /// </summary>
    /// <param name="deltaStack">Stacks change to update</param>
    public void UpdatePropertyStacks(int deltaStack)
    {

        if (deltaStack == 0)
            return;

        if (deltaStack > 1)
        {
            if (_modType == ModValueType.Additive)
                _property.AddAdditive("", _baseValue, deltaStack);
            else
                _property.AddMultiplier("", _baseValue, deltaStack);
        }
        else
        {
            if (_modType == ModValueType.Additive)
                _property.RemoveAdditive("", deltaStack);
            else
                _property.RemoveMultiplier("", deltaStack);
        }

    }

}

