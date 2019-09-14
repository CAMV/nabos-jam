using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Class <c> SquasController </c> handles ther order given to a squad or group of unit.
/// </summary>
public class PlayerSquadController : SquadController
{
    // The order of the units defines who goes where in a formation.


    /// <summary>
    /// Units currently active in the squad
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
    protected override void Update()
    {
        base.Update();
    }


    /// <summary>
    /// Updates the select gizmos of the units of the party.
    /// </summary>
    private void UpdateSelectGizmo()
    {
        foreach(Unit u in _myUnits)
        {
            if (u.SelectGizmo)
            {
                if (ActiveUnits.Contains(u))
                {
                    u.SelectGizmo.SetIntensity(USelectGizmo.SelectGizmoIntensity.High);
                }
                else if (u.IsFollower)
                {
                    u.SelectGizmo.SetIntensity(USelectGizmo.SelectGizmoIntensity.Low);
                }
                else
                {
                    u.SelectGizmo.SetIntensity(USelectGizmo.SelectGizmoIntensity.Medium);
                }
            }
        }       
    }

}
