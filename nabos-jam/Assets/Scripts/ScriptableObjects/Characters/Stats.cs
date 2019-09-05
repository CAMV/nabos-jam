using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    List<Stat> stats;
    public Stat damage;
    public Stat attackSpeed;
    public Stat hitChance;
    public Stat dodgeChance;

    private void Update() {
        //Reduces buff durations
        float tick = Time.fixedDeltaTime;
        foreach (var stat in stats)
        {
            stat.TickModifiers(tick);
        }
        
    }

    public bool HitCheck() 
    {
        float randVal = Random.Range(0, 100);
        return (hitChance.IsStatGreater((int) randVal));
    }

    public bool DodgeCheck()
    {
        float randVal = Random.Range(0, 100);   
        return (dodgeChance.IsStatLower((int) randVal));
    }
}
