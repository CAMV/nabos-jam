using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Class <c> SquasController </c> handles ther order given to a party or group of unit.
/// </summary>
public class PlayerPartyController : PartyController
{
    // The order of the units defines who goes where in a formation.


    /// <summary>
    /// Units currently active in the party
    /// </summary>
    public override List<Unit> ActiveUnits {
        get {
            return _activeUnits;
        }

        set {
            setActiveUnits(value);
            if (value.Count > 0)
            {
                foreach (Unit u in value)
                {
                    if (_myUnits.Contains(u.RootLeader))
                    {
                        UpdateSelectGizmo();
                        break;
                    }

                }
            }
        }
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UpdateSelectGizmo();
    }

    // Update is called once per frame
    protected void Update()
    {
    }


    /// <summary>
    /// Updates the select gizmos of the units of the party.
    /// </summary>
    private void UpdateSelectGizmo()
    {
        foreach(Unit u in _myUnits)
        {
            if (u.Gizmo.SelectGizmo != null)
            {
                if (ActiveUnits.Contains(u))
                {
                    u.Gizmo.SelectGizmo.SetIntensity(UnitGizmoIntensity.High);
                }
                else if (u.IsFollower)
                {
                    u.Gizmo.SelectGizmo.SetIntensity(UnitGizmoIntensity.Low);
                }
                else
                {
                    u.Gizmo.SelectGizmo.SetIntensity(UnitGizmoIntensity.Medium);
                }
            }
        }       
    }

}
