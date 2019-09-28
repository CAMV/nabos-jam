using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Stop Moving")]
public class StopMovingAction : Action
{

    public override void PerformAction() {
        if (_unit && _unit.Movements)  
        {
            _unit.Movements.StopMoving();
        }
    }
}
