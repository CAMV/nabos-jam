using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private SquadController _playerSquad;

    public SquadController PlayerSquad {
        get {
            return _playerSquad;
        }
    }

    void Update()
    { 

    }


}