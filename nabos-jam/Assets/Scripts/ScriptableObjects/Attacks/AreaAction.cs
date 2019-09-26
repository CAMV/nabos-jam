using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class that models an action that ocurrs in an area
/// </summary>
public class AreaAction : ActionObject
{
    private List<Unit> _affectedUnits = new List<Unit>();

    //////////////// METHODS ////////////////

    /// <summary>
    /// Tick that updates the lifespan of the area and apply the effects.
    /// </summary>
    protected override void UpdateTick(object sender, TickEventArgs e)
    {
        base.UpdateTick(sender, e);

        foreach (var unit in _affectedUnits)
        {
            ExecuteAction(unit, e.value);
            SendModifiers(unit);
        }

    }

    /// <summary>
    /// Action to be executed when the attack object makes contact to a valid target.
    /// </summary>
    protected void ExecuteAction(Unit targetUnit, float tick)
    {
        SendModifiers(targetUnit);

        for (int i = 0; i < _damages.Length; i++)
        {
            _damages[i].ApplyDamageAsTimeRate(targetUnit, tick);
        }

    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        // Check if objects is initialzed
        if (!_isInit)
            return;

        if (!IsValidCollider(other))
            return;

        Unit targetUnit = other.gameObject.GetComponentInParent<Unit>();
        
        _affectedUnits.Add(targetUnit);
    }

    public void OnTriggerExit(Collider other)
    {
                // Check if objects is initialzed
        if (!_isInit)
            return;

        if (!IsValidCollider(other))
            return;

        Unit targetUnit = other.gameObject.GetComponentInParent<Unit>();
        
        if (_affectedUnits.Contains(targetUnit))
            _affectedUnits.Remove(targetUnit);
            
    }
}