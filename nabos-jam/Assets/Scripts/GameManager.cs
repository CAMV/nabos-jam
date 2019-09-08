using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// singleton that manages the game states as well as access to diferent key strucutures
/// </summary>
public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private SquadController _playerSquad = null;

    /// <summary>
    /// Player's squad controller
    /// </summary>
    public SquadController PlayerSquad {
        get {
            return _playerSquad;
        }
    }

    void Start()
    { 
        // Selects the first unit at the begining so a unit is always selected.
        _playerSquad.AddCommand(new SelectCmd(new List<Unit>() {_playerSquad.Units[0]} ));
    }


}