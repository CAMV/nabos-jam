using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : AttackObject
{

    [SerializeField]
    protected float _force = 1;
    protected Rigidbody _myRigidBody;

    /// <summary>
    /// Initialize the projectile position and movement
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="target"></param>
    public override void Initialize(Unit parent, Vector3 target)
    {
        _parentUnit = parent;
        _myRigidBody.AddForce(transform.forward * Vector3.Distance(parent.transform.position, target) *_force);
    }

    void Awake()
    {
        _myRigidBody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Send the attack msj to the valid objet
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator ExecuteAttack()
    {
        if (_targetUnit)
        {
            _targetUnit.Character.TakeDamage(_damage);
            
            yield return new WaitForEndOfFrame();

            transform.parent = _targetUnit.transform;
            _myRigidBody.isKinematic = true;
            _myRigidBody.velocity = Vector3.zero;
            _myRigidBody.angularVelocity = Vector3.zero;
            GameObject.Destroy(gameObject, 10f);    
        }
    }

    /// <summary>
    /// Execute action when projectile lands on a World object
    /// </summary>
    protected override void OnWorldCollision()
    {
        _myRigidBody.isKinematic = true;
        _myRigidBody.velocity = Vector3.zero;
        _myRigidBody.angularVelocity = Vector3.zero;
        GameObject.Destroy(gameObject, 5f);
    }

}