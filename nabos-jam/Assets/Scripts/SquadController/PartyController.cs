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
                MoveParty(
                            _activeUnits, 
                            _activeUnits[0].transform.position,
                            _activeUnits[0].transform.rotation,
                            _myFormation
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
        _activeUnits = new List<Unit>();
        _isIdle = true;
    }

    /// <summary>
    /// Move the units given of the squad to a given position, ith a given final rotation and an optional formation to follow.
    /// </summary>
    /// <param name="units">Units to move</param>
    /// <param name="tPosition">Target position</param>
    /// <param name="rotation">Target rotation</param>
    /// <param name="formation">Optional formation</param>
    public void MoveParty(List<Unit> units, Vector3 tPosition, Quaternion rotation, Formation formation = null)
    {
        var _tRotation = rotation;
        var _hasRotation = true;

        List<Vector3> _calculatedPositions = new List<Vector3>();
        List<Quaternion> _calculatedRotations = new List<Quaternion>();

        // If not rotations given, calculate it based on a look at rotation from the start point(defacto leader position) to the end point of the movement
        if (!_hasRotation)
        {
            _tRotation = units[0].transform.rotation;

            Vector3 endPoint;

            if (_calculatedPositions.Count == 0)
                endPoint = tPosition;
            else
                endPoint = _calculatedPositions[0];

            _tRotation.SetLookRotation(
                (endPoint - units[0].transform.position).normalized,
                Vector3.up
            );
            
        }

        Matrix4x4 tPosMatrix = new Matrix4x4();
        int nUnitsToMove = GameManager.Instance.PlayerParty.ActiveUnits.Count;

        // Calculate leader transform matrix and, if need, the target positions
        if (_calculatedPositions.Count == 0)
        {
            tPosMatrix.SetTRS(tPosition, _tRotation, Vector3.one);

            // Add leader position
            _calculatedPositions.Add(tPosition);

            // Calculate and add non-leader positions
            for(int i = 0; i < nUnitsToMove - 1; i++)
            {
                _calculatedPositions.Add(Formation.GetFollowerPosition(
                    (Formation)formation, 
                    i,
                    tPosMatrix
                ));
            }

        } 
        else 
            tPosMatrix.SetTRS(_calculatedPositions[0], _tRotation, Vector3.one);

        
        // If needed, calculate the target rotations
        if (_calculatedRotations.Count == 0)
        {
            _calculatedRotations.Add(_tRotation);

            for(int i = 0; i < nUnitsToMove - 1; i++)
            {
                _calculatedRotations.Add(Formation.GetFollowerRotation(
                        (Formation)formation, 
                        i,
                        tPosMatrix
                    ));
            }
        }      

        // Add single unit move commands
        for(int i = 0; i < nUnitsToMove; i++)
        {
            MoveUnit(
                GameManager.Instance.PlayerParty.ActiveUnits[i], 
                _calculatedPositions[i],
                _calculatedRotations[i]
            );
        }
    }

    /// <summary>
    /// Moves a given unit to a target postion.
    /// </summary>
    /// <param name="unit">Unit to move</param>
    /// <param name="targetPos">Target position</param>
    /// <param name="direction">Target rotation</param>
    public void MoveUnit(Unit unit, Vector3 targetPos, Quaternion direction)
    {
        if (!unit.Movements)
            Debug.Log(unit.name + " does not have a Movement component!");
        else
            unit.Movements.Move(targetPos, direction);

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
            if (GUIManager.Instance.PlayerAvatarsHandler)
                GUIManager.Instance.PlayerAvatarsHandler.SetSelectedAvatars(value);
            
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

            MoveParty(
                    _activeUnits, 
                    leaderPos,
                    leaderRot,
                    _myFormation
                );

            return true;
        }

        return false;
    }
}
