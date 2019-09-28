using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class that models an action wich object is thrown
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ProjectileAction : ActionObject
{

    [SerializeField]
    private bool _isFollowingTarget = false;

    [SerializeField]
    private int _destroyDelay = 0;

    [SerializeField]
    protected float _mySpeed;

    protected Rigidbody _myRigidBody;
    protected Transform _myTarget; 

    /// <summary>
    /// Initialize the projectile position and movement
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="target"></param>
    public override void Initialice(Unit unit)
    {
        base.Initialice(unit);

        _parentUnit = unit;
        _myRigidBody = GetComponent<Rigidbody>();
        _isInit = true;
        
    }

    /// <summary>
    /// Launchs the projectile to a given target with a given speed.
    /// </summary>
    /// <param name="target">Target to be hitted by the projectile.</param>
    /// <param name="speed">Speed of the projectile movment.</param>
    public void Launch(Vector3 target, float speed = 0)
    {
        if (speed > 0)
            _mySpeed = speed;

        transform.rotation = Quaternion.LookRotation(Vector3.Normalize(target - transform.position), Vector3.up);

        if (_isFollowingTarget)
            StartCoroutine(FollowTargetCO());            
        else
            _myRigidBody.AddRelativeForce(0, 0, speed);

        if (!_myRigidBody.useGravity)
            return;

        float distance = Vector3.Distance(transform.position, target);
        float time = distance/speed;
        float yForce = target.y - transform.position.y - (-9.8f*time*time);
        yForce /= time;
        _myRigidBody.AddForce(0, yForce,0);

    }

    public void Launch(Transform unitTransform, float speed = 0) 
    {
        Launch(unitTransform.position, speed);
    }

    /// <summary>
    /// Send the attack msj to the valid objet
    /// </summary>
    /// <returns></returns>
    protected override void ExecuteAction(Unit target)
    {
        return;
    }

    /// <summary>
    /// Coroutine executed if the projectile followe the target.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private IEnumerator FollowTargetCO()
    {
        while (Vector3.Distance(_myTarget.position, transform.position) > 0)
        {
            transform.rotation = Quaternion.LookRotation(_myTarget.position - transform.position, Vector3.up);
            transform.position += Vector3.Normalize(_myTarget.position - transform.position) * _mySpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        StopCoroutine(FollowTargetCO());

        base.OnTriggerEnter(other);

        _myRigidBody.isKinematic = true;
        _myRigidBody.velocity = Vector3.zero;

        Destroy(this, _destroyDelay);
    }
}