using UnityEngine;
using System.Collections;

public class MeleeAction : ActionObject
{
    /// <summary>
    /// atach the object to a given transform that should be the active part of a weapon.
    /// </summary>
    /// <param name="weapon">Transform to be atached.</param>
    public void AttachToWeapon(Transform weapon)
    {
        transform.parent = weapon;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}