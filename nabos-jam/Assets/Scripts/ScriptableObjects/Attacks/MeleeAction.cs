using UnityEngine;
using System.Collections;

/// <summary>
/// Action that ocurr if an specific object touches a unit
/// </summary>
public class MeleeAction : ActionObject
{
    [SerializeField]
    private UnitPart _unitPart;

    /// <summary>
    /// Same initalice but attach the object to the weapon to follow it during the attack animation.
    /// </summary>
    /// <param name="unit"></param>
    override public void Initialice(Unit unit){
        base.Initialice(unit);
        transform.parent = _parentUnit.Animation[_unitPart];
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    } 
}