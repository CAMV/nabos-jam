using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

/// <summary>
/// Models a unit in the game, linking the unit to its different components.
/// </summary>
public class Unit : MonoBehaviour
{
    [SerializeField]
    private Character _myChar = null;
    
    [SerializeField]
    protected CharacterBio _myCharBio = null;

    [SerializeField]
    protected UMovement _movement = null;

    [SerializeField]
    protected USelectGizmo _selectGizmo = null;

    [SerializeField]
    private UVectorGizmo _vectorGizmo = null;

    [SerializeField]
    private UAreaGizmo _areaGizmo = null;


    [SerializeField]
    protected Collider _selectCollider = null;

    [SerializeField]
    protected USkills _skills = null;

    protected List<Unit> _followers;
    // If the unit is part of a formations, tis variable holds its leader.
    private Unit _leader;

    /// <summary>
    /// Unit component that handles its skills.
    /// </summary>
    public USkills Skills {
        get {
            return _skills;
        }
    }

    /// <summary>
    /// The character associated to the unit.
    /// </summary>
    public Character Character {
        get {
            return _myChar;
        }
    }

    /// <summary>
    /// The cosmetic info of the character for GUI purposes.
    /// </summary>
    public CharacterBio Bio {
        get {
            return _myCharBio;
        }
    }

    /// <summary>
    /// The movement component of the unit, if it has it.
    /// </summary>
    public UMovement Movement{
        get {
            return _movement;
        }
    }

    /// <summary>
    /// The units that is the leader of the local formation.
    /// </summary>
    public Unit Leader {
        get {
            return _leader;
        }
        set {
            _leader = value;
        }
    }

    /// <summary>
    /// Returns the unit with the highest hierarchy in the formation.
    /// </summary>
    public Unit RootLeader {
        get {
            if (Leader == null)
                return this;
            else
                return this.Leader.RootLeader;
            
        }
    }

    /// <summary>
    /// The select gizmo component of the unit, if it has it.
    /// </summary>
    public USelectGizmo SelectGizmo{
        get {
            return _selectGizmo;
        }
    }

    /// <summary>
    /// The vector gizmo component of the unit, if it has it.
    /// </summary>
    public UVectorGizmo VectorGizmo{
        get {
            return _vectorGizmo;
        }
    }

    /// <summary>
    /// The area gizmo component of the unit, if it has it.
    /// </summary>
    public UAreaGizmo AreaGizmo{
        get {
            return _areaGizmo;
        }
    }

    /// <summary>
    /// The collider component of the unit, if it has it.
    /// </summary>
    public Collider SelectCollider {
        get {
            return _selectCollider;
        }
    }

    /// <summary>
    /// Return true if the unit is leader of a local formation.
    /// </summary>
    public bool IsLeader {
        get {
            return _followers.Count > 0;
        }
    }

    /// <summary>
    /// Returns true if is follower of a local formation.
    /// </summary>
    public bool IsFollower {
        get {
            return _leader == null;
        }
    }

    /// <summary>
    /// Returns true if the Unit does not have any leader.
    /// </summary>
    public bool IsRootLeader {
        get {
            return this.RootLeader == this;
        }
    }

    /// <summary>
    /// Adds a given unit as a follower of the unit.
    /// </summary>
    /// <param name="u">Unit to be added as follower.</param>
    public void AddFollower(Unit u)
    {
        if (!u.HasFollower(this))
        {
            _followers.Add(u);
            u.Leader = this;
        }
    }

    /// <summary>
    /// Removes a given unit as a follower.
    /// </summary>
    /// <param name="u">Unit to be removed from the follower.</param>
    public void RemoveFollower(Unit u)
    {
        if (_followers.Contains(u))
        {
            _followers.Remove(u);
            u.Leader = null;
        }
    }

    /// <summary>
    /// Returns true if the given Unit is a follower of the Unit.
    /// </summary>
    /// <param name="u">Unit to ckeck if is a follower.</param>
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

    protected virtual void Start()
    {
        _leader = null;
        _followers = new List<Unit>();
    }
}