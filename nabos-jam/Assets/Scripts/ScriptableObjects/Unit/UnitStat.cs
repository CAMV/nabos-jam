using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Class that represents a Unit's Stat. It depends on attributes to be calculated.
/// </summary>
[CreateAssetMenu(menuName = "Properties/Stat")]   
public class UnitStat : UnitProperty
{
    [SerializeField]
    private List<UnitAttribute>  _attDependendcy = new List<UnitAttribute>();
    [SerializeField]
    private List<float> _weightDependency = new List<float>();

    //////////////// METHODS ////////////////
    
    /// <summary>
    /// Calculates the current value of the Stat.
    /// </summary>
    /// <param name="baseValue">Value to use as base value.</param>
    /// <returns>Value after adding all modifiers and dependency of the stat.</returns>
    override protected int CalculateCurrentValue(float baseValue)
    {
        int finalValue = base.CalculateCurrentValue(baseValue);

        float totalAttribute = 0;

        for (int i = 0; i < _attDependendcy.Count; i++)
            totalAttribute += _attDependendcy[i].CurrentValue * _weightDependency[i];
            
        finalValue +=  Mathf.RoundToInt(_baseValue*totalAttribute);

        return finalValue; 
    } 

    /// <summary>
    /// Initializes the attribute. Used right after instantiating the attribute scriptable object.
    /// </summary>
    /// <param name="unit">Unit that holds the instance of the attribute.</param>
    /// <param name="baseValue">Initia base value of the attribute.</param>
    public override void Initialize(Unit unit, int baseValue)
    {
        base.Initialize(unit, baseValue);

        List<int> indexesToRemove = new List<int>();

        // Get the unit's instance of the dependancy attributes

        for (int i = 0; i < _attDependendcy.Count; i++)
        {
            UnitAttribute unitAttribute = unit.Properties.GetAttribute(_attDependendcy[i]); 
            
            if (unitAttribute)
            {
                _attDependendcy[i] = unitAttribute;
                unitAttribute.ValueChanged += OnDependencyChange;
            }
            else
                indexesToRemove.Add(i);
        }

        // If an attribute is not found, remove it from the dependencies.

        int indexOffset = 0;

        for (int i = 0; i < indexesToRemove.Count; i++)
        {
            _attDependendcy.RemoveAt(indexesToRemove[i] - indexOffset);
            _weightDependency.RemoveAt(indexesToRemove[i] - indexOffset);
            indexOffset++;
        }
    }

    /// <summary>
    /// Send a ValuedChanged event when the attribute changes. To be assigned to the evenHandler of the attribute.`
    /// </summary>
    protected void OnDependencyChange(object sender, EventArgs e)
    {
        OnValueChanged();
    }
}
