using UnityEngine;

public class UAttackComponent : MonoBehaviour 
{
    [SerializeField]
    public Skill _basicAttack;

    private bool _isInit = false;
    private bool _isAttacking = false;
    private Unit _myUnit;
    private UnitStat _attackSpeed;

    private Unit _targetUnit;

    /// <summary>
    /// Returns the current target of the unit. 
    /// </summary>
    public Unit Target {
        get {
            return _targetUnit;
        }
    }

    /// <summary>
    // Flag that indicates if the unit is attacking
    /// </summary>
    public bool IsAttacking {
        get {
            return _isAttacking;
        }
    }

    //////////////// METHODS ////////////////

    public void Initialize()
    {
        _basicAttack = ScriptableObject.Instantiate(_basicAttack);
        _basicAttack.Initialize(_myUnit);
        _isInit = true;
    }

    /// <summary>
    /// Checks if the target unit is within the target range
    /// </summary>
    /// <returns>True if it is in range, false otherwise.</returns>
    public bool CheckIfInRange()
    {
        if (Vector3.Distance(_myUnit.transform.position, _targetUnit.transform.position) > _basicAttack.Range)
            return false;

        RaycastHit hit;
        bool isRaycatsSuccessful = false;

        if (Physics.Raycast(_myUnit.Animation[UnitPart.Head].position, _targetUnit.Animation[UnitPart.Head].position, out hit, 100))
            isRaycatsSuccessful |= hit.collider.GetComponentInParent<Unit>() == _targetUnit;
        
        if (!isRaycatsSuccessful && Physics.Raycast(_myUnit.Animation[UnitPart.Center].position, _targetUnit.Animation[UnitPart.Center].position, out hit, 100))
            isRaycatsSuccessful |= hit.collider.GetComponentInParent<Unit>() == _targetUnit;

        if (!isRaycatsSuccessful && Physics.Raycast(_myUnit.Animation[UnitPart.Base].position, _targetUnit.Animation[UnitPart.Base].position, out hit, 100))
            isRaycatsSuccessful |= hit.collider.GetComponentInParent<Unit>() == _targetUnit;

        if (!isRaycatsSuccessful)
            return false;

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    public void Attack(Unit target)
    {
        if (!_isInit)
            return;

        _targetUnit = target;
        _isAttacking = true;

        if (!CheckIfInRange())
            _myUnit.Movements.MoveWithinRange();

        _basicAttack.Cast();
    }

    public void CancelAttack()
    {
        _targetUnit = null;
        _isAttacking = false;
    }

    void Start()
    {
        _myUnit = GetComponent<Unit>();
        Initialize();
    }
}