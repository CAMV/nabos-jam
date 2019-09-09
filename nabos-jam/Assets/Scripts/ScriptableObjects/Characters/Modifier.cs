using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modificator types. Additive modificators are added to the base value,
/// which then is multiplied by the multiplicative values.
/// </summary>
public enum ModType{
    Additive,
    //Treat multiplicative values as a base 1 percentage. 
    //e.g a 40% increase would be a multiplicative value of 1.4
    Multiplicative      
}

/// <summary>
/// Class the hold relevant information for modifying a <c>Stat</c> class
/// </summary>
[CreateAssetMenu(menuName = "Character/Stat/Modifier")]
public class Modifier: ScriptableObject
{
    public string modifierName;
    public float modValue;
    public ModType modType; //Additive or Multiplicative

    [SerializeField]
    public bool isStackable {get; private set;}     //False means the modifier will be overwritten

    //Timer interactions
    public float timeToLive;    //Initial time duration, or a negative value if infinite
    private float timeLeft;     //Time left before expiry

    Modifier(string modName, float val, int ttl, bool _isStackable, ModType _modType) 
    {
        timeToLive = ttl;
        modValue = val;
        modType = _modType;
        modifierName = modName;
        isStackable = _isStackable;
    }

    /// <summary>
    /// Reduce the modifier's remaining time
    /// </summary>
    /// <param name="tickValue">A timestep</param>
    /// <returns>the remaining duration, or -1 if the buff is infinite</returns>
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
