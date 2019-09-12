using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Class <c> SquasController </c> handles ther order given to a squad or group of unit.
/// </summary>
public class SquadController : MonoBehaviour
{
    // The order of the units defines who goes where in a formation.
    [SerializeField]
    private List<Unit> _myUnits = new List<Unit>();

    private Formation _myFormation = null;
    private List<Unit> _activeUnits = new List<Unit>();
    private Queue<Command> _cmdQ;
    private bool _isIdle = true;

    /// <summary>
    /// Units that cinforms the squad
    /// </summary>
    public List<Unit> Units {
        get {
            return _myUnits;
        }
    }

    /// <summary>
    /// Meta fromation that the squad follows when moving.
    /// </summary>
    public Formation Formation {
        get {
            return _myFormation;
        }
        set {
            _myFormation = value;

            if (_activeUnits.Count > 0)
                AddCommand(
                        new MoveSquadCmd(
                                    _activeUnits, 
                                    _activeUnits[0].transform.position,
                                    _activeUnits[0].transform.rotation,
                                    _myFormation
                                )
                        );
        }
    }

    /// <summary>
    /// Size of the Squad
    /// </summary>
    public int Size {
        get {
            return _myUnits.Count;
        }
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

    /// <summary>
    /// Units currently active in the squad
    /// </summary>
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
                if (GUIManager.Instance.SquadUnitsGUI)
                    GUIManager.Instance.SquadUnitsGUI.SetSelectedAvatars(value);
                
            }
        }
    }

    /// <summary>
    /// Swaps ther position of two given units in the list.
    /// </summary>
    /// <param name="firstU"></param>
    /// <param name="secondU"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Adds a command to be executed by the party.
    /// </summary>
    /// <param name="c">Command to be executed.</param>
    public void AddCommand(Command c)
    {
        _cmdQ.Enqueue(c);
    }
}
