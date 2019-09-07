using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private SquadController _playerSquad = null;

    public SquadController PlayerSquad {
        get {
            return _playerSquad;
        }
    }

    void Update()
    { 

    }


}