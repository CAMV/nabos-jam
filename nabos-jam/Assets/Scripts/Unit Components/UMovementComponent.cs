using UnityEngine;
using System;
using System.Collections;
using UnityEngine.AI;

/// <summary>
/// Unit component that handles the movement of the unit.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class UMovementComponent : MonoBehaviour
{
    private NavMeshAgent _myNavAgent;

    private Quaternion _targetRotation;
    private Unit _myUnit;

    // Move in Range vars
    void Awake()
    {
        _myNavAgent = GetComponent<NavMeshAgent>();
        _myUnit = GetComponentInParent<Unit>();
    }

    /// <summary>
    /// Executes the move behaviour given a target position to move.
    /// </summary>
    /// <param name="target">Target destination for the movement.</param>
    public void Move(Vector3 target, bool cancelMovement = false)
    {
        if (cancelMovement)
        {
            StopCoroutine(MoveWithinRangeCO());
            StopCoroutine(ApplyRotation());
        }

        _myNavAgent.ResetPath();
        _myNavAgent.SetDestination(target);
    }

    /// <summary>
    /// Moves the unit until is within the basic attack range of the unit
    /// </summary>
    public void MoveWithinRange()
    {
        StopCoroutine(MoveWithinRangeCO());
    }

    private IEnumerator MoveWithinRangeCO()
    {
        Vector3 targetPos = _myUnit.Attacks.Target.transform.position;
        NavMeshPath path = new NavMeshPath();

        Move(targetPos);
        
        while (!_myUnit.Attacks.CheckIfInRange() || _myNavAgent.CalculatePath(targetPos, path))
        {
            if (!_myUnit.Attacks.Target)
                break;

            if (targetPos != _myUnit.Attacks.Target.transform.position)
            {
                targetPos = _myUnit.Attacks.Target.transform.position;
                Move(targetPos);
            }

            yield return new WaitForEndOfFrame();
        }

        if (_myUnit.Attacks.Target && _myUnit.Attacks.IsAttacking)
            _myUnit.Attacks.Attack(_myUnit.Attacks.Target);

   }

    /// <summary>
    /// Executes the move behaviour given a target position to move and a final rotation to have at the end of the movement.
    /// </summary>
    /// <param name="target">Executes the move behaviour given a target position to move.</param>
    /// <param name="rotation">Rotation to have at the end of the movement.</param>
    public void Move(Vector3 target, Quaternion rotation, bool cancelMovement = false)
    {
        Move(target, cancelMovement);
        _targetRotation = rotation;
        StartCoroutine(ApplyRotation());
    }

    /// <summary>
    /// Stops unit movement
    /// </summary>
    public void StopMoving()
    {
        _myNavAgent.ResetPath();
    }

    public bool isMoving()
    {
        if (!_myNavAgent.pathPending)
        {
            if (_myNavAgent.remainingDistance <= _myNavAgent.stoppingDistance)
            {
                if (!_myNavAgent.hasPath || _myNavAgent.velocity.sqrMagnitude == 0f)
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Coroutines that aplies the rotation at the end of the movement.
    /// </summary>
    /// <param name="rotation">Rotation to set at the end of the movement.</param>
    /// <returns></returns>
    private IEnumerator ApplyRotation()
    {
        
        while (_myNavAgent.remainingDistance > _myNavAgent.stoppingDistance)
        {
            yield return new WaitForEndOfFrame();
        }

        transform.rotation = _targetRotation;
    }


}