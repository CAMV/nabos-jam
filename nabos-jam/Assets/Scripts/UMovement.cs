using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UMovement : MonoBehaviour
{
    private NavMeshAgent _myNavAgent;

    public NavMeshAgent Agent{
        get {
            return _myNavAgent;
        }
    }

    public void Move(Vector3 target)
    {
        Agent.ResetPath();
        _myNavAgent.SetDestination(target);
    }

    public void Move(Vector3 target, Quaternion rotation)
    {
        Agent.ResetPath();
        _myNavAgent.SetDestination(target);
        StartCoroutine(ApplyRotation(rotation));
    }

    private IEnumerator ApplyRotation(Quaternion rotation)
    {
        
        while (_myNavAgent.remainingDistance > _myNavAgent.stoppingDistance)
        {
            yield return new WaitForEndOfFrame();
        }

        transform.rotation = rotation;
        Debug.Log(name);
    }

    void Start()
    {
        _myNavAgent = GetComponent<NavMeshAgent>();
    }
}