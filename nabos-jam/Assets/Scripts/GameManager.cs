using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private SquadController _playerSquad = null;

    public SquadController PlayerSquad {
        get {
            return _playerSquad;
        }
    }

    void Start()
    { 
        _playerSquad.AddCommand(new SelectCmd(new List<Unit>() {_playerSquad.Units[0]} ));
    }


}