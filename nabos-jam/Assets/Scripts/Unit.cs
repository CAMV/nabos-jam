using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Unit : MonoBehaviour
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