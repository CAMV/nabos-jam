using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat: ScriptableObject
{
    public string statName;
    public int statValue {get; private set;}
    public int finalStat {get; private set;}
    public List<Modifier> modifiers = new List<Modifier>();

    public Stat(string _statName, int _statValue) 
    {
        statName = _statName;
        statValue = _statValue;
    }

    public void setStat(int baseStat)
    {
        statValue = baseStat;
    }

    public void AddModifier(Modifier _mod)
    {
        foreach (var mod in modifiers)
        {
            if (mod.modifierName == _mod.modifierName)
            {
                if (mod.isStackable)
                {
                    modifiers.Add(_mod);
                    CalculateFinalStat();
                }
                else 
                {
                    modifiers.Remove(mod);
                    modifiers.Add(mod);
                }
            }
        }

    }

    public void TickModifiers(float tick) 
    {
        //Gets the original length
        int initialLength = modifiers.Count;
        //Removes expired modifiers
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

    public void CalculateFinalStat() 
    {
        float additiveCount = (float) statValue;
        float multiplicativeCount = 1f;

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

    public bool IsStatGreater(int val) 
    {
        return finalStat > val;
    }

    public bool IsStatLower(int val) 
    {
        return finalStat < val;
    }
}
