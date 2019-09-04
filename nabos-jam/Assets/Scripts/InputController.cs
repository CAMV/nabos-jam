using UnityEngine;
using System.Collections.Generic;

public class InputController : MonoBehaviour
{


    [SerializeField]
    private SquadController _playerSquad;
    [SerializeField]
    private List<InputAction> _inputActions;

    private Unit _activeUnit;

    public void Start()
    {
        _activeUnit = _playerSquad.ActiveUnit;
    }

    public void Update()
    {
        foreach(var input in _inputActions)
        {
            if (input.CheckInput())
            {
                Command c = input.GetInputCommand();

                if (c != null)
                    _playerSquad.AddCommand(c);
            }

        }
    }
}