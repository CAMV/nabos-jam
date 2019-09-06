using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModType{
    Additive,
    Multiplicative
}

[CreateAssetMenu(menuName = "Character/Stat/Modifier")]
public class Modifier: ScriptableObject
{
    public string modifierName;
    public float modValue;
    public ModType modType;

    [SerializeField]
    public bool isStackable {get; private set;} 

    //Timer interactions
    public float timeToLive;
    private float timeLeft;

    Modifier() {
        timeLeft = timeToLive;
    }

    Modifier(string modName, float val, int ttl, bool _isStackable, ModType _modType) 
    {
        timeToLive = ttl;
        modValue = val;
        modType = _modType;
        modifierName = modName;
        isStackable = _isStackable;
    }

    //TODO This whole timer interaction can be a timer script outside 
    public float Tick(float tickValue) 
    {
        //Infinite buffs
        if (timeToLive < 0) 
        {
            return -1;
        }


        timeLeft -= tickValue;
        if (timeLeft < 0) 
        {
            timeLeft = 0;
        }
        return timeLeft;
    }
}
