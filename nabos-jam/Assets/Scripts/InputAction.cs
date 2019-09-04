using UnityEngine;

public abstract class InputAction : ScriptableObject
{
    [SerializeField]
    private string _name;

    private bool _currentState;
    private bool _prevState;

    public bool CheckInput()
    {
        _prevState = _currentState;
        _currentState = Input.GetButtonDown(_name); 
        
        return (!_prevState && _currentState);        
    }

    public abstract Command GetInputCommand(Unit u);
    
}