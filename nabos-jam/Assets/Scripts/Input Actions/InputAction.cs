using UnityEngine;
using System.Collections;

public abstract class InputAction : ScriptableObject
{
    protected const float RAYCAST_LENGTH = 100;

    [SerializeField]
    protected string _buttomName;
    [SerializeField]
    private InputCheckType _myICheckType;


    enum InputCheckType
    {
        OnPress, AfterPress
    }

    private bool _currentState;
    private bool _prevState;
    private bool _isModifierActive;

    public bool CheckInput()
    {
        _prevState = _currentState;
        _currentState = Input.GetButton(_buttomName); 
        
        if (_myICheckType == InputCheckType.AfterPress)
            return (_prevState && !_currentState);

        return (!_prevState && _currentState);           
    }

    public abstract IEnumerator ExecuteAction();
    
}