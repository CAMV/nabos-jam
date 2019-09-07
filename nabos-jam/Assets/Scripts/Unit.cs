using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    private NavMeshAgent _myNavAgent;
    public Character character;
    [SerializeField]
    private UMovement _movement = null;

    [SerializeField]
    private USelectGizmo _gizmo = null;

    [SerializeField]
    private Collider _selectCollider = null;

    private List<Unit> _followers;
    private Unit _leader;

    public UMovement Movement{
        get {
            return _movement;
        }
    }

    public Unit Leader {
        get {
            return _leader;
        }
        set {
            _leader = value;
        }
    }

    public Unit RootLeader {
        get {
            if (Leader == null)
                return this;
            else
                return this.Leader.RootLeader;
            
        }
    }

    public USelectGizmo Gizmo{
        get {
            return _gizmo;
        }
    }

    public Collider SelectCollider {
        get {
            return _selectCollider;
        }
    }

    public bool IsLeader {
        get {
            return _followers.Count > 0;
        }
    }

    public bool IsFollower {
        get {
            return _leader == null;
        }
    }

    public bool IsRootLeader {
        get {
            return this.RootLeader == this;
        }
    }

    public void AddFollower(Unit u)
    {
        if (!u.HasFollower(this))
        {
            _followers.Add(u);
            u.Leader = this;
        }
    }

    public void RemoveFollower(Unit u)
    {
        if (_followers.Contains(u))
        {
            _followers.Remove(u);
            u.Leader = null;
        }
    }

    public bool HasFollower(Unit u)
    {
        if (_followers.Contains(u))
            return true;

        foreach(var v in _followers)
        {
            if (v.HasFollower(u))
                return true;
        }

        return false;         
    }

    void Start()
    {
        _leader = null;
        _followers = new List<Unit>();
    }
}