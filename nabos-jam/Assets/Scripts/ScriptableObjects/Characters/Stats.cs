﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    List<Stat> baseStats = new List<Stat>();    //Str, Int, Agi, Dex, etc
    List<Stat> evasionStats = new List<Stat>(); //Block, Dodge, Negate, etc
    public Stat damage;
    public Stat armor;
    public Stat attackSpeed;
    public Stat hitChance;

    private void Update() {
        //Reduces buff durations
        float tick = Time.fixedDeltaTime;
        foreach (var stat in baseStats)
        {
            stat.TickModifiers(tick);
        }
        foreach (var stat in evasionStats)
        {
            stat.TickModifiers(tick);
        }
        damage.TickModifiers(tick);
        armor.TickModifiers(tick);
        attackSpeed.TickModifiers(tick);
        hitChance.TickModifiers(tick);
        
    }

    public Stat getStatValue(string statName)
    {
        foreach (var stat in baseStats)
        {
            if (stat.statName == statName)
            {
                return stat;
            }
        }
        return null;
    }

    public void updateDamage() 
    {
        Stat str = getStatValue("Strength");
        damage.setStat(str.finalStat*2);
        damage.CalculateFinalStat();
    }

    public bool HitCheck() 
    {
        float randVal = Random.Range(0, 100);
        return (hitChance.IsStatGreater((int) randVal));
    }

    //Checks if any of the evasion stats passes
    public bool hasDodged()
    {
        foreach (var evasionStat in evasionStats)
        {
            float randVal = Random.Range(0, 100);   
            bool evade = evasionStat.IsStatLower((int) randVal);
            if (evade)
            {
                return evade;
            }
        }
        return false;
    }


}
