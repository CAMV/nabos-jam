using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{

    [SerializeField]
    private List<Unit> _myUnits;

    private Unit _activeUnit;
    private Queue<Command> _cmdQ;
    private bool _isIdle = true;

    public Unit ActiveUnit {
        get {
            if (!_activeUnit)
                _activeUnit = _myUnits[0];
            
            return _activeUnit;
        }

        set {
            if (_activeUnit != value && _myUnits.Contains(value.RootLeader))
            {
                _activeUnit = value.RootLeader;
                UpdateSelectGizmo();
            }
        }
    }

    public List<Unit> Members {
        get {
            return _myUnits;
        }
    }

    private void UpdateSelectGizmo()
    {
        foreach(Unit u in _myUnits)
        {
            if (u == ActiveUnit)
            {
                u.Gizmo.SetIntensity(USelectGizmo.SelectGizmoIntensity.High);
            }
            else if (u.IsFollower)
            {
                u.Gizmo.SetIntensity(USelectGizmo.SelectGizmoIntensity.Low);
            }
            else
            {
                u.Gizmo.SetIntensity(USelectGizmo.SelectGizmoIntensity.Medium);
            }
        }
    }

    public void AddCommand(Command c)
    {
        _cmdQ.Enqueue(c);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _cmdQ = new Queue<Command>();
        UpdateSelectGizmo();
        _isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_cmdQ.Count > 0 && _isIdle)
        {
            _cmdQ.Dequeue().Do();
        }
    }
}
