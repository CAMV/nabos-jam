using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Class that represents a Unit Attribute. Should not be instantiated in runtime after an stat that depends on it.
/// </summary>
[CreateAssetMenu(menuName = "Properties/Attribute")]  
public class UnitAttribute : UnitProperty
{
    //////////////// PROPERTIES ////////////////

    /// <summary>
    /// Sets the base value.
    /// </summary>
    public int BaseValue {
        get {
            return Mathf.RoundToInt(_baseValue);
        }
        set {
            _baseValue = value;

            OnValueChanged();
        }
    }
}