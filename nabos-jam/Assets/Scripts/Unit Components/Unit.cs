using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

/// <summary>
/// Models a unit in the game, linking the unit to its different components.
/// </summary>
public class Unit : MonoBehaviour
{
    // [SerializeField]
    // private Character _myChar = null;
    
    [SerializeField]
    protected GUIData _GUIData = null;

    // gizmos
    [SerializeField]
    protected UGizmos _gizmos = null;

    // Components

    [SerializeField]
    protected Collider _selectCollider = null;

    [SerializeField]
    protected UMovementComponent _movement = null;

    [SerializeField]
    protected USkillsComponent _skills = null;

    [SerializeField]
    protected UPropertyComponent _properties = null;
    
    [SerializeField]
    protected UAttackComponent _attack = null;
    
    [SerializeField]
    protected UModifiersComponent _modifiers = null;

    [SerializeField]
    protected UAnimatorComponent _animations = null;

    // Formation Data

    protected List<Unit> _followers;
    // If the unit is part of a formations, tis variable holds its leader.
    private Unit _leader;

    //////////////// PROPERTIES ////////////////

    //  Componentes properties

    /// <summary>
    /// Info of the character for GUI purposes.
    /// </summary>
    public GUIData GUIData {
        get {
            return _GUIData;
        }
    }

    /// <summary>
    /// The movement component of the unit, if it has it.
    /// </summary>
    public UMovementComponent Movements{
        get {
            return _movement;
        }
    }

    /// <summary>
    /// The attack component of the unit, if it has it.
    /// </summary>
    public UAttackComponent Attacks{
        get {
            return _attack;
        }
    }

    /// <summary>
    /// The skill component of the unit, if it has it.
    /// </summary>
    public USkillsComponent Skills {
        get {
            return _skills;
        }
    }

    /// <summary>
    /// The modifier component of the unit, if it has it.
    /// </summary>
    public UModifiersComponent Modifiers {
        get {
            return _modifiers;
        }
    }

    
    /// <summary>
    /// The properties component of the unit, if it has it.
    /// </summary>
    public UPropertyComponent Properties {
        get {
            return _properties;
        }
    }

    /// <summary>
    /// The animations component of the unit, if it has it.
    /// </summary>
    public UAnimatorComponent Animation {
        get {
            return _animations;
        }
    }

    // Gizmo Components

    /// <summary>
    /// The select gizmo component of the unit, if it has it.
    /// </summary>
    public UGizmos Gizmo{
        get {
            return _gizmos;
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

    // Formation properties

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

    //////////////// METHODS ////////////////

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

    protected virtual void Awake()
    {
        _leader = null;
        _followers = new List<Unit>();

        if (_properties)
            _properties.Initialize();
    }
}