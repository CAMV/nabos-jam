using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{

    [SerializeField]
    private List<Unit> _myUnits;
    [SerializeField]
    private Formation _myFormation;

    private List<Unit> _activeUnits;
    private Queue<Command> _cmdQ;
    private bool _isIdle = true;

    public List<Unit> ActiveUnits {
        get {
            if (_activeUnits.Count == 0)
                _activeUnits.Add(_myUnits[0]);
            
            return _activeUnits;
        }

        set {
            if (value.Count > 0)
            {
                bool hasValidUnit = false;

                foreach(Unit u in value)
                {
                    if (_myUnits.Contains(u.RootLeader))
                    {
                        hasValidUnit = true;
                        break;
                    }
                }

                if (hasValidUnit)
                {
                    _activeUnits.Clear();

                    // only root leaders can be an active unit
                    foreach(Unit u in value) {
                        if (_myUnits.Contains(u.RootLeader) && !_activeUnits.Contains(u.RootLeader))
                            _activeUnits.Add(u.RootLeader);
                    }
                    
                    UpdateSelectGizmo();
                }
            }
        }
    }

    public List<Unit> Units {
        get {
            return _myUnits;
        }
    }

    public Formation Formation
    {
        get {
            return _myFormation;
        }
    }

    public bool SwapUnitsOrer(int firstU, int secondU)
    {
        if (firstU > 0 && firstU < _myUnits.Count && firstU > 0 && firstU < _myUnits.Count && firstU != secondU)
        {
            Unit tmp = _myUnits[firstU];
            _myUnits[firstU] = _myUnits[secondU];
            _myUnits[secondU] = tmp;

            return true;
        }

        return false;
    }

    private void UpdateSelectGizmo()
    {
        foreach(Unit u in _myUnits)
        {
            if (ActiveUnits.Contains(u))
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
        _activeUnits = new List<Unit>();
        UpdateSelectGizmo();
        _isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_cmdQ.Count > 0 && _isIdle)
        {
            Command currentCmd = _cmdQ.Dequeue();

            currentCmd.Do();
        }
    }
}
