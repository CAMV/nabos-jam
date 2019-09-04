using UnityEngine;
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

    void Start()
    {
        _myNavAgent = GetComponent<NavMeshAgent>();
    }
}