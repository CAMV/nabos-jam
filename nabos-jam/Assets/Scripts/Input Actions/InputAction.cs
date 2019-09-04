using UnityEngine;

public abstract class InputAction : ScriptableObject
{
    [SerializeField]
    private string _buttomName;
    [SerializeField]
    private InputCheckType _myICheckType;
    protected const float RAYCAST_LENGTH = 100;

    enum InputCheckType
    {
        OnPress, AfterPress
    }

    private bool _currentState;
    private bool _prevState;

    public bool CheckInput()
    {
        _prevState = _currentState;
        _currentState = Input.GetButton(_buttomName); 
        
        if (_myICheckType == InputCheckType.AfterPress)
            return (_prevState && !_currentState);

        return (!_prevState && _currentState);           
    }

    public abstract Command GetInputCommand();
    
}