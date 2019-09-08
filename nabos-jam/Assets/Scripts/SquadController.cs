using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SquadController : MonoBehaviour
{

    [SerializeField]
    private List<Unit> _myUnits = new List<Unit>();
    [SerializeField]
    private Formation _myFormation = null;

    private List<Unit> _activeUnits = new List<Unit>();
    private Queue<Command> _cmdQ;
    private bool _isIdle = true;

    
    public List<Unit> Units {
        get {
            return _myUnits;
        }
    }

    public Formation Formation {
        get {
            return _myFormation;
        }
    }

    public int Size {
        get {
            return _myUnits.Count;
        }
    }

    public List<Unit> ActiveUnits {
        get {
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

                    // Order active units folllowing _myUnits order
                    SortedList sActiveUnits = new SortedList();

                    // only root leaders can be an active unit
                    foreach(Unit u in value) {
                        if (_myUnits.Contains(u.RootLeader) && !_activeUnits.Contains(u.RootLeader))
                            sActiveUnits.Add(_myUnits.IndexOf(u.RootLeader), u.RootLeader);
                    }

                    _activeUnits = sActiveUnits.GetValueList().Cast<Unit>().ToList();

                    UpdateSelectGizmo();
                }

                // Update GUI
                GUIManager.Instance.SquadUnitsGUI.SetActiveAvatars(value);
                
            }
        }
    }

    public bool SwapUnitsOrer(int firstU, int secondU)
    {
        if (firstU >= 0 && firstU < _myUnits.Count && secondU >= 0 && secondU < _myUnits.Count && firstU != secondU)
        {
            Unit tmp = _myUnits[firstU];
            _myUnits[firstU] = _myUnits[secondU];
            _myUnits[secondU] = tmp;

            Vector3 leaderPos = _activeUnits[0].transform.position;
            Quaternion leaderRot = _activeUnits[0].transform.rotation;

            // Order active units folllowing _myUnits order
            SortedList sActiveUnits = new SortedList() ;

            // only root leaders can be an active unit
            foreach(Unit u in _activeUnits)
                sActiveUnits.Add(_myUnits.IndexOf(u.RootLeader), u.RootLeader);

            _activeUnits = sActiveUnits.GetValueList().Cast<Unit>().ToList();

            AddCommand(
                    new MoveSquadCmd(
                                _activeUnits, 
                                leaderPos,
                                leaderRot,
                                _myFormation
                            )
                    );


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
