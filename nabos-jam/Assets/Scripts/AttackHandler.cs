using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles attack interactions
/// </summary>
public class AttackHandler : MonoBehaviour
{
    public Character character;
    public bool isAttacking = false;
    
    private void FixedUpdate() 
    {
        //Reduce character cooldowns
        float tick = Time.fixedDeltaTime;
        character.TickDurations(tick);
    }
}
