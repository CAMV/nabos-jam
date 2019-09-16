using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Command that moves a group of units, decomposing the command in different MoveCmd for each unit.
/// </summary>
public class MovePartyCmd : Command
{
    private List<Unit> _unitsToMove;
    private Formation _formation;
    private Vector3 _tPosition;
    private Quaternion _tRotation;
    private bool _hasRotation;

    private List<Vector3> _calculatedPositions;
    private List<Quaternion> _calculatedRotations;

    /// <summary>
    /// Constructs the commmand given a target position and a target rotation for the whole party.
    /// </summary>
    /// <param name="units">Units to be moved</param>
    /// <param name="position">Target position for the whole party</param>
    /// <param name="rotation">Target rotation for the whole party</param>
    /// <param name="formation">Formation to be followed by the party</param>
    public MovePartyCmd(List<Unit> units, Vector3 position, Quaternion rotation, Formation formation = null)
    {
        _unitsToMove = units;
        _formation = formation;
        _tPosition = position;
        _tRotation = rotation;
        _hasRotation = true;

        _calculatedPositions = new List<Vector3>();
        _calculatedRotations = new List<Quaternion>();
    }

    /// <summary>
    /// Constructs the commmand given a target position for the whole party.
    /// </summary>
    /// <param name="units">Units to be moved</param>
    /// <param name="position">Target position for the whole party</param>
    /// <param name="formation">Formation to be followed by the party</param>
    public MovePartyCmd(List<Unit> units, Vector3 position, Formation formation = null)
    {
        _unitsToMove = units;
        _formation = formation;
        _tPosition = position;
        _hasRotation = false;

        _calculatedPositions = new List<Vector3>();
        _calculatedRotations = new List<Quaternion>();
    }

    /// <summary>
    /// Constructs the command giving the positions and rotations for each unit.
    /// </summary>
    /// <param name="units"Units to be moved</param>
    /// <param name="positions">Positions for each unit</param>
    /// <param name="rotations">Rotations for each unit</param>
    public MovePartyCmd(List<Unit> units, List<Vector3> positions, List<Quaternion> rotations)
    {
        _unitsToMove = units;
        _calculatedPositions = positions;
        _calculatedRotations = rotations;
        _hasRotation = true;
    }

    /// <summary>
    /// Constructs the command giving the positions for each unit.
    /// </summary>
    /// <param name="units"Units to be moved</param>
    /// <param name="positions">Positions for each unit</param>
    /// <param name="formation">Formation to be followed by the party</param>
    public MovePartyCmd(List<Unit> units, List<Vector3> positions, Formation formation = null)
    {
        _unitsToMove = units;
        _formation = formation;
        _calculatedPositions = positions;
        _calculatedRotations = new List<Quaternion>();
        _hasRotation = false;
    }

    /// <summary>
    /// Decompose the command into several MoveCmd for each unit setted.
    /// </summary>
    override public void Do()
    {

        // If not rotations given, calculate it based on a look at rotation from the start point(defacto leader position) to the end point of the movement
        if (!_hasRotation)
        {
            _tRotation = _unitsToMove[0].transform.rotation;

            Vector3 endPoint;

            if (_calculatedPositions.Count == 0)
                endPoint = _tPosition;
            else
                endPoint = _calculatedPositions[0];

            _tRotation.SetLookRotation(
                (endPoint - _unitsToMove[0].transform.position).normalized,
                Vector3.up
            );
            
        }

        Matrix4x4 tPosMatrix = new Matrix4x4();
        int nUnitsToMove = GameManager.Instance.PlayerParty.ActiveUnits.Count;

        // Calculate leader transform matrix and, if need, the target positions
        if (_calculatedPositions.Count == 0)
        {
            tPosMatrix.SetTRS(_tPosition, _tRotation, Vector3.one);

            // Add leader position
            _calculatedPositions.Add(_tPosition);

            // Calculate and add non-leader positions
            for(int i = 0; i < nUnitsToMove - 1; i++)
            {
                _calculatedPositions.Add(Formation.GetFollowerPosition(
                    _formation, 
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
                        _formation, 
                        i,
                        tPosMatrix
                    ));
            }
        }      

        // Add single unit move commands
        for(int i = 0; i < nUnitsToMove; i++)
        {

            Command fMoveCmd;

            fMoveCmd = new MoveCmd(
                    GameManager.Instance.PlayerParty.ActiveUnits[i], 
                    _calculatedPositions[i],
                    _calculatedRotations[i]
                );
            
            GameManager.Instance.PlayerParty.AddCommand(fMoveCmd);
        }
    }

    /// <summary>
    /// Undo any movement done by this command. DOES NOTHING!!!
    /// </summary>
    override public void Undo()
    {   
    }

}
