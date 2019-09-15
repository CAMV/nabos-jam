using UnityEngine;
using System.Collections;

/// <summary>
/// Class that represent an attack as a game oject that collides againts targets.
/// </summary>
[RequireComponent(typeof(Collider))]
public abstract class AttackObject : MonoBehaviour 
{
    // Types of triggers for the attack
    public enum AttackTriggerType {
        All, EvilNpc, GoodNpc, OtherGroup
    }

    [SerializeField]
    protected int _damage;

    [SerializeField]
    protected AttackTriggerType _triggerType;
  
    protected Unit _targetUnit = null;
    protected Unit _parentUnit = null;

    /// <summary>
    /// Initialize the object with the origin unit and the vector target.
    /// </summary>
    /// <param name="parent">Unit that sends the object.</param>
    /// <param name="target">Target where to throw the object in world space coord.</param>
    public abstract void Initialize(Unit parent, Vector3 target);

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(SphereCollider))
            return;
            
        Debug.Log("AJA");
        bool isTrigger = false;

        if (other.tag == "World" || other.tag == "Floor")
        {
            OnWorldCollision();
            return;
        }

        switch (_triggerType)
        {
            case AttackTriggerType.All:
                isTrigger = true;
            break;
            case AttackTriggerType.EvilNpc:
                isTrigger = other.tag == "EvilNpc";
            break;
            case AttackTriggerType.GoodNpc:
                isTrigger = other.tag == "GoodNpc";
            break;
            case AttackTriggerType.OtherGroup:
                isTrigger = other.tag != gameObject.tag;
            break;
        }

        if (isTrigger)
        {
            _targetUnit = other.gameObject.GetComponentInParent<Unit>();
            StartCoroutine(ExecuteAttack());
        }
    }

    /// <summary>
    /// Action to be executed when the attack object makes contact to a valid target.
    /// </summary>
    protected abstract IEnumerator ExecuteAttack();

    /// <summary>
    /// Action to be executed when an attack Object lands on a world object.
    /// </summary>
    protected abstract void OnWorldCollision();

}