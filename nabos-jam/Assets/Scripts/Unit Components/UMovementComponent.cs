using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/// <summary>
/// Unit component that handles the movement of the unit.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class UMovementComponent : MonoBehaviour
{
    private NavMeshAgent _myNavAgent;

    void Awake()
    {
        _myNavAgent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Executes the move behaviour given a target position to move.
    /// </summary>
    /// <param name="target">Target destination for the movement.</param>
    public void Move(Vector3 target)
    {
        _myNavAgent.ResetPath();
        _myNavAgent.SetDestination(target);
    }

    /// <summary>
    /// Executes the move behaviour given a target position to move and a final rotation to have at the end of the movement.
    /// </summary>
    /// <param name="target">Executes the move behaviour given a target position to move.</param>
    /// <param name="rotation">Rotation to have at the end of the movement.</param>
    public void Move(Vector3 target, Quaternion rotation)
    {
        _myNavAgent.ResetPath();
        _myNavAgent.SetDestination(target);
        StartCoroutine(ApplyRotation(rotation));
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
    private IEnumerator ApplyRotation(Quaternion rotation)
    {
        
        while (_myNavAgent.remainingDistance > _myNavAgent.stoppingDistance)
        {
            yield return new WaitForEndOfFrame();
        }

        transform.rotation = rotation;
    }


}