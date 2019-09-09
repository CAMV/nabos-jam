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

    /// <summary>
    /// Get a list of the formation availables to the player
    /// </summary>
    public List<Formation> FormationAvailable {
        get {
            return _formationsAvailable;
        }
    }

    /// <summary>
    /// Changes the formation of the plaer's squad from the list of available formation. 
    /// </summary>
    /// <param name="option">Index of the formation available</param>
    public void ChangePlayerSquadFormation(int option)
    {
        if (_formationsAvailable.Count > 0 && option >= 0 && option < _formationsAvailable.Count)
            _playerSquad.Formation = _formationsAvailable[option];
    }

    void Start()
    { 
        // Selects the first unit at the begining so a unit is always selected.
        _playerSquad.AddCommand(new SelectCmd(new List<Unit>() {_playerSquad.Units[0]}));
        // Assign the first available formation to the player squad.
        ChangePlayerSquadFormation(0);
    }


}