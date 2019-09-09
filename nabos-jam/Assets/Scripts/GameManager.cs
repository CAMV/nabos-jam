using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Singleton that manages the game states as well as access to diferent key strucutures
/// </summary>
public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private SquadController _playerSquad = null;

    [SerializeField]
    private List<Formation> _formationsAvailable = new List<Formation>();

    /// <summary>
    /// Player's squad controller
    /// </summary>
    public SquadController PlayerSquad {
        get {
            return _playerSquad;
        }
    }

    public List<Formation> FormationAvailable {
        get {
            return _formationsAvailable;
        }
    }

    public void ChangePlayerSquadFormation(int option)
    {
        if (option >= 0 && option < _formationsAvailable.Count)
            _playerSquad.Formation = _formationsAvailable[option];
    }

    void Start()
    { 
        // Selects the first unit at the begining so a unit is always selected.
        _playerSquad.AddCommand(new SelectCmd(new List<Unit>() {_playerSquad.Units[0]}));
        // Assign the first available formation to the player squad.
        if (_formationsAvailable.Count > 0)
            _playerSquad.Formation = _formationsAvailable[0];
    }


}