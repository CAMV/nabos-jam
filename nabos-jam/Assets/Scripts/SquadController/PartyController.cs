using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Class <c> SquasController </c> handles ther order given to a party or group of unit.
/// </summary>
public class PartyController : MonoBehaviour
{
    // The order of the units defines who goes where in a formation.
    [SerializeField]
    protected List<Unit> _myUnits = new List<Unit>();

    protected Formation _myFormation = null;
    protected List<Unit> _activeUnits = new List<Unit>();
    protected Queue<Command> _cmdQ;
    protected bool _isIdle = true;

    /// <summary>
    /// Units that cinforms the party
    /// </summary>
    public List<Unit> Units {
        get {
            return _myUnits;
        }
    }

    /// <summary>
    /// Meta fromation that the party follows when moving.
    /// </summary>
    public Formation Formation {
        get {
            return _myFormation;
        }
        set {
            _myFormation = value;

            if (_activeUnits.Count > 0)
                AddCommand(
                        new MovePartyCmd(
                                    _activeUnits, 
                                    _activeUnits[0].transform.position,
                                    _activeUnits[0].transform.rotation,
                                    _myFormation
                                )
                        );
        }
    }

    /// <summary>
    /// Size of the Party
    /// </summary>
    public int Size {
        get {
            return _myUnits.Count;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _cmdQ = new Queue<Command>();
        _activeUnits = new List<Unit>();
        _isIdle = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_cmdQ.Count > 0 && _isIdle)
        {
            Command currentCmd = _cmdQ.Dequeue();

            currentCmd.Do();
        }
    }

    /// <summary>
    /// Set current active units in the squat
    /// </summary>
    /// <param name="value"></param>
    protected void setActiveUnits(List<Unit> value)
    {
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

            }

            // Update GUI
            if (GUIManager.Instance.PartyUnitsGUI)
                GUIManager.Instance.PartyUnitsGUI.SetSelectedAvatars(value);
            
        }
    }

    /// <summary>
    /// Units currently active in the party
    /// </summary>
    public virtual List<Unit> ActiveUnits {
        get {
            return _activeUnits;
        }

        set {
            setActiveUnits(value);
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
                    new MovePartyCmd(
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
    /// Adds a command to be executed by the party.
    /// </summary>
    /// <param name="c">Command to be executed.</param>
    public void AddCommand(Command c)
    {
        _cmdQ.Enqueue(c);
    }
}
