using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Random Move")]
public class RandomMoveAction : Action
{

    public override void PerformAction() {
        if (_unit && _unit.Movement)  {
            Vector2 newMovePoint = Random.insideUnitCircle * _unit.Character.lineOfSight;
            Vector3 newPosition = _unit.transform.position 
                                    + new Vector3(newMovePoint.x, _unit.transform.position.y, newMovePoint.y);
            Debug.Log("moving to position " + newPosition);
            _unit.Movement.Move(newPosition);
        }
    }
}
