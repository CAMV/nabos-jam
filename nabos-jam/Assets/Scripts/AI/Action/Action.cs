using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    [SerializeField]    
    public bool usesUnitInfo;
    [SerializeField]
    protected Unit _unit;
    

    public void Initialize(Unit unit) {_unit = unit;}
    public abstract void PerformAction();

}
