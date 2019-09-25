using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICommandHandler : MonoBehaviour
{
    [SerializeField]
    private float dontPerformActionRange = 0;
    [SerializeField]

    private void Start()
    {
    }

        // public void ExecuteAction(AIUnit u) {
        //     AttackHandler attackHandler = u.GetComponent<AttackHandler>();
        //     if (attackHandler && attackHandler.isAttacking) {

        //     }
        //     else
        //     {
        //         float performActionValue = Random.Range(0f,1f);
        //         if (performActionValue >= dontPerformActionRange)
        //         {
        //             if (u.Movement != null)  {
        //                 Vector2 newMovePoint = Random.insideUnitCircle * u.Character.lineOfSight;
        //                 Vector3 newPosition = transform.position + new Vector3(newMovePoint.x, transform.position.y, newMovePoint.y);
        //                 u.Movement.Move(newPosition);
        //                 u.IsIdle = false;
        //             }
        //         }
        //     }
        // }
}
