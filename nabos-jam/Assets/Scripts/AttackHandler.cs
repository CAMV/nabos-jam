using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public Character character;
    
    private void FixedUpdate() 
    {
        float tick = Time.fixedDeltaTime;
        character.TickDurations(tick);
    }
}
