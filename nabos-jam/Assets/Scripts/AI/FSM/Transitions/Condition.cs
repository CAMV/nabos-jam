using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : ScriptableObject
{
    [SerializeField]
    public bool usesUnitInfo;
    [SerializeField]
    protected Unit _unit;
    [SerializeField]
    protected bool negateCondition;       //Used when you want to negate the condition


    public virtual void Initialize(Unit unit)  {_unit = unit;}
    public abstract bool IsTriggered();

}
