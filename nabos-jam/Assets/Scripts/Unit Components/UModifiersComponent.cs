using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UModifiersComponent : MonoBehaviour 
{
    [SerializeField]
    private List<Modifier> _modifiers = new List<Modifier>();
    [SerializeField]
    private List<int> _initStacks = new List<int>();

    private Unit _myUnit;

    //////////////// METHODS ////////////////

    /// <summary>
    /// Given the unit that holds the componenet, initialize the modifier component.
    /// </summary>
    /// <param name="unit">Unit of the component.</param>
    public void Initialize(Unit unit)
    {
        _myUnit = unit;
        
        for (int i = 0; i < _modifiers.Count; i++)
        {
            _modifiers[i] = ScriptableObject.Instantiate(_modifiers[i]);
            _modifiers[i].Initialize(unit, _initStacks[i]);
        }
    }

    /// <summary>
    /// Given a modifier and a int reference, returns true if the modififer is present and the index of it in the int reference.
    /// </summary>
    /// <param name="mod">Modifier to find</param>
    /// <param name="index">Index of the modifier</param>
    /// <returns>True if its present with the index value, False if it's not present, with an -1 as the index</returns>
    public bool ContainsModifier(Modifier mod, out int index)
    {
        for (int i = 0; i < _modifiers.Count; i++)
        {
            if (_modifiers[i].GUIData.name == mod.GUIData.name)
            {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
    }

    /// <summary>
    /// Adds a given modifier with a given number of initial stacks. If the modifier is already in the list, add stacks to the modifier. 
    /// </summary>
    /// <param name="mod">Modifier to add</param>
    /// <param name="stacks">Stacks to add</param>
    public void AddModifier(Modifier mod, int stacks = 1)
    {
        int index;
        if (ContainsModifier(mod, out index))
            _modifiers[index].ChangeStacks(stacks);
        else
        {
            Modifier m = ScriptableObject.Instantiate(mod);
            m.Initialize(_myUnit, stacks);
            _modifiers.Add(mod);
        }   

    }

    /// <summary>
    /// Removes a given modifier from the list.
    /// If it has stacks, remove all stacks first and waits for the onDepleatedStack event removes the modifier.
    /// </summary>
    /// <param name="modifier">Modifier to remove</param>
    public void RemoveModifier(Modifier modifier)
    {
        int index;
        if (ContainsModifier(modifier, out index))
        {
            if (_modifiers[index].Stacks > 0)
                _modifiers[index].ChangeStacks(-_modifiers[index].Stacks);
            else
                _modifiers.RemoveAt(index);
        }    
    }
}