using UnityEngine;

public class UAnimatorComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _head = null;

    [SerializeField]
    private Transform _eyes = null;

    [SerializeField]
    private Transform _lFoot = null;
    
    [SerializeField]
    private Transform _rFoot = null;

    [SerializeField]
    private Transform _lHand = null;

    [SerializeField]
    private Transform _rHand = null;

    [SerializeField]
    private Transform _base = null;

    [SerializeField]
    private Transform _center = null;

    [SerializeField]
    private Transform _weaponBase = null;

    [SerializeField]
    private Transform _weaponPoint = null;
    //////////////// INDEXER ////////////////

    /// <summary>
    /// Return the part of a tranform where a part of the body using as indexer the UnitPart Type
    /// </summary>
    public Transform this[UnitPart index] {
        get {
            switch(index)
            {
                case UnitPart.Center:
                    return _center;
                case UnitPart.Head:
                    return _head;
                case UnitPart.Eyes:
                    return _eyes;
                case UnitPart.LeftFoot:
                    return _lFoot;
                case UnitPart.RightFoot:
                    return _rFoot;
                case UnitPart.LeftHand:
                    return _lHand;
                case UnitPart.RightHand:
                    return _rHand;
                case UnitPart.WeaponBase:
                    return _weaponBase;
                case UnitPart.WeaponPoint:
                    return _weaponPoint;
                default:
                    return _base;
            }
        }

    }

    //////////////// PROPERTIES ////////////////

}