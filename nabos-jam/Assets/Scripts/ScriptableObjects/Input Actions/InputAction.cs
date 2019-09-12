using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Class <c> InputAction </c> models an action executed when an specific input is recivied.
/// </summary>
public abstract class InputAction : ScriptableObject
{
    protected const float RAYCAST_LENGTH = 100;

    // Name of the Input Axes in the Input Setting of the project
    [SerializeField]
    protected string _buttomName;
    [SerializeField]
    private InputCheckType _myICheckType = InputCheckType.OnPress;

    enum InputCheckType
    {
        OnPress, AfterPress
    }

    private bool _currentState;
    private bool _prevState;
    private bool _isModifierActive;

    /// <summary>
    /// Checks if the Input Axes of the Input action is active
    /// </summary>
    /// <returns>is the input active</returns>
    public virtual bool CheckInput()
    {
        _prevState = _currentState;
        _currentState = Input.GetButton(_buttomName); 
        
        if (_myICheckType == InputCheckType.AfterPress)
            return (_prevState && !_currentState);

        return (!_prevState && _currentState);           
    }

    /// <summary>
    /// Coroutine that executes the action asociated with the input
    /// </summary>
    public abstract IEnumerator ExecuteAction();
    
}