using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/RandomMove")]
public class RandomMoveAction : Action
{
    [SerializeField]
    Unit unit;

    public override void PerformAction() {
        if (unit && unit.Movement)  {
            Vector2 newMovePoint = Random.insideUnitCircle * unit.Character.lineOfSight;
            Vector3 newPosition = unit.transform.position 
                                    + new Vector3(newMovePoint.x, unit.transform.position.y, newMovePoint.y);
            unit.Movement.Move(newPosition);
        }
    }
}
