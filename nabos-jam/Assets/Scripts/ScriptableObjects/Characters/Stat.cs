using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds information on a single character stat
/// </summary>
[CreateAssetMenu(menuName = "Character/Stat/Stat")]
public class Stat: ScriptableObject
{
    public string statName;
    public int statValue;       //base value
    public int finalStat {get; private set;}        //Final value after modifiers have been added
    public List<Modifier> modifiers = new List<Modifier>();     //List of all available modifiers
    //TODO permanent modifiers could have a separate list

    public Stat(string _statName, int _statValue) 
    {
        statName = _statName;
        statValue = _statValue;
        finalStat = _statValue;
    }

    private void OnEnable()
    {
        CalculateFinalStat();
    }        

    /// <summary>
    /// Adds a new <c>Modifier</c> to the list
    /// </summary>
    /// <param name="_mod">The modifier to be added</param>
    public void AddModifier(Modifier _mod)
    {
        //Dont readd non-stackable modifiers
        if (!_mod.isStackable) {
            //Checks if the modifier exists
            foreach (var mod in modifiers)
            {
                //Refresh the duration and exit
                if (mod.modifierName == _mod.modifierName)
                {
                    modifiers.Remove(mod);
                    modifiers.Add(mod);
                    return;
                }
            }
        }

        //Add the modifier if it is stackable or it didn't exist
        modifiers.Add(_mod);
        CalculateFinalStat();
    }

    /// <summary>
    /// Reduce modifier durations
    /// </summary>
    /// <param name="tick">A timestep</param>
    public void TickModifiers(float tick) 
    {
        //Gets the original length
        int initialLength = modifiers.Count;
        //Refresh and remove expired modifiers
        for (int i = modifiers.Count-1; i >= 0; i--)
        {
            Modifier mod = modifiers[i];
            if (mod.Tick(tick) == 0) 
            {
                modifiers.RemoveAt(i);
            }

        }

        //If modifiers were removed, recalculate the stat
        if (initialLength != modifiers.Count) 
        {
            CalculateFinalStat();
        }
    }

    /// <summary>
    /// Update the final stat based on its active modifiers
    /// </summary>
    public void CalculateFinalStat() 
    {
        float additiveCount = (float) statValue;    //Additive accumulator  
        float multiplicativeCount = 1f;             //Multiplicative accumulator

        foreach (var mod in modifiers) 
        {
            if (mod.modType == ModType.Additive)
            {
                additiveCount += mod.modValue;
            }
            else if (mod.modType == ModType.Multiplicative)
            {
                multiplicativeCount *= mod.modValue;
            }
        }
        finalStat = (int) (additiveCount * multiplicativeCount);
    }

    /// <summary>
    /// Checks if a given value is greater than the stats final value
    /// </summary>
    /// <param name="val">an integer to compare</param>
    /// <returns>The comparison check</returns>
    public bool IsStatGreater(int val) 
    {
        return finalStat > val;
    }

    /// <summary>
    /// Checks if a given value is lower than the stats final value
    /// </summary>
    /// <param name="val">an integer to compare</param>
    /// <returns>The comparison check</returns>
    public bool IsStatLower(int val) 
    {
        return finalStat < val;
    }
}
