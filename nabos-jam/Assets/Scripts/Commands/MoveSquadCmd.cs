using UnityEngine;
using System.Collections.Generic;

public class MoveSquadCmd : Command
{

    private List<Unit> _unitsToMove;
    private Formation _formation;
    private Vector3 _tPosition;
    private Quaternion _tRotation;
    private bool _hasRotation;

    public MoveSquadCmd(List<Unit> units, Vector3 position, Quaternion rotation, Formation formation = null)
    {
        _unitsToMove = units;
        _formation = formation;
        _tPosition = position;
        _tRotation = rotation;
        _hasRotation = true;
    }

    public MoveSquadCmd(List<Unit> units, Vector3 position, Formation formation = null)
    {
        _unitsToMove = units;
        _formation = formation;
        _tPosition = position;
        _hasRotation = false;
    }

    override public void Do()
    {
        if (!_hasRotation)
        {
            _tRotation = _unitsToMove[0].transform.rotation;
            _tRotation.SetLookRotation(
                (_tPosition - _unitsToMove[0].transform.position).normalized,
                Vector3.up
            );
        }

        List<Quaternion> rotations = new List<Quaternion>();
        List<Vector3> positions = new List<Vector3>();

        rotations.Add(_tRotation);
        positions.Add(_tPosition);

        // Calculate non-leader units target position/rotation        
        Matrix4x4 tPosMatrix = new Matrix4x4();
        tPosMatrix.SetTRS(_tPosition, _tRotation, Vector3.one);

        for(int i = 0; i < GameManager.Instance.PlayerSquad.ActiveUnits.Count - 1; i++)
        {
            rotations.Add(Formation.GetFollowerRotation(
                    _formation, 
                    i,
                    tPosMatrix
                ));

            positions.Add(Formation.GetFollowerPosition(
                _formation, 
                i,
                tPosMatrix
            ));
        }

        // Add single unit move commands
        for(int i = 0; i < GameManager.Instance.PlayerSquad.ActiveUnits.Count; i++)
        {

            Command fMoveCmd;

            // apply formation position offset
            if (_formation)
            {
                fMoveCmd = new MoveCmd(
                        GameManager.Instance.PlayerSquad.ActiveUnits[i], 
                        positions[i],
                        rotations[i]
                    );
            }
            else
            {
                fMoveCmd = new MoveCmd(
                            GameManager.Instance.PlayerSquad.ActiveUnits[i], 
                            positions[i]
                        );
            }
            
            GameManager.Instance.PlayerSquad.AddCommand(fMoveCmd);
        }

    }

    override public void Undo()
    {
        
    }

}
