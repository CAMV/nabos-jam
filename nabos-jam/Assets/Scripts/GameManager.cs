using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Singleton that manages the game states as well as access to diferent key strucutures
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private PartyController _playerParty = null;

    [SerializeField]
    private List<Formation> _formationsAvailable = new List<Formation>();

    /// <summary>
    /// Player's party controller
    /// </summary>
    public PartyController PlayerParty {
        get {
            return _playerParty;
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

    void Start()
    { 
        // Selects the first unit at the begining so a unit is always selected.
        _playerParty.AddCommand(new SelectCmd(new List<Unit>() {_playerParty.Units[0]}));
        // Assign the first available formation to the player party.
        ChangePlayerPartyFormation(0);
    }

    /// <summary>
    /// Changes the formation of the plaer's party from the list of available formation. 
    /// </summary>
    /// <param name="option">Index of the formation available</param>
    public void ChangePlayerPartyFormation(int option)
    {
        if (_formationsAvailable.Count > 0 && option >= 0 && option < _formationsAvailable.Count)
            _playerParty.Formation = _formationsAvailable[option];
    }


}