using UnityEngine;
using System;

/// <summary>
/// Module of a modifier that handles its stacks.
/// </summary>
[Serializable]
public class ModifierStackComponent
{
    [SerializeField]
    private int _maxStacks = 1;

    [SerializeField]
    private bool _areStacksRemovedAtOnce = false;

    [SerializeField]
    private float _cdToAdd = 0;

    private float _currentCDToAdd = 0;
    private int _numStacks = 0;
    private Func<int, bool> _OnStackChanged;


    //////////////// PROPERTIES ////////////////

    /// <summary>
    /// Returns amount of the attacks
    /// </summary>
    public int Value{
        get {
            return _numStacks;
        }
    }

    /// <summary>
    /// Returns true if the stacks are removed all at once
    /// </summary>
    public bool AreStacksRemovedAtOnce {
        get {
            return _areStacksRemovedAtOnce;
        }
    }

    //////////////// METHODS ////////////////

    /// <summary>
    /// Initialize the stacks, setting the event caller with the given listener
    /// </summary>
    /// <param name="OnStackChanged">Function to call when the stack changes</param>
    public void Initialize(Func<int, bool> OnStackChanged)
    {
        _OnStackChanged = OnStackChanged; 
    } 

    
    /// <summary>
    /// Tries to add a given number of stacks to the modifier, by default one. if succesfull, returns true. Otherwise, false.
    /// </summary>
    /// <param name="stacks">Number of stacks to add</param>
    /// <returns>True if succefully added an stack, false otherwise.</returns>
    public void AddStack(int stacks = 1)
    {
        if (stacks <= 0)
            return;

        if (_numStacks >= _maxStacks)
        {
            _numStacks = _maxStacks;
            _currentCDToAdd = _cdToAdd;
            return;
        }

        if (_currentCDToAdd > 0)
            return;

        _numStacks = _numStacks + stacks > _maxStacks ? _maxStacks : _numStacks + stacks;
        _currentCDToAdd = _cdToAdd;
        GameManager.Instance.tickHandler += UpdateCDToAdd;

        _OnStackChanged(_numStacks);
    }

    /// <summary>
    /// Removes a given number of stacks, by default one. If all stacks are removed, call the OnDepleated Event.
    /// </summary>
    /// <param name="stacks">Number of stacks to remove</param>
    public void RemoveStack(int stacks = 1)
    {
        if (stacks <= 0)
            return;


        int deltaStack = _numStacks;

        if (_areStacksRemovedAtOnce || _numStacks - stacks <= 0)
        {
            _numStacks = 0;
            _OnStackChanged(-deltaStack);
            GameManager.Instance.tickHandler -= UpdateCDToAdd;
        }
        else
        {
            deltaStack = -stacks;
            _numStacks -= stacks;
            _OnStackChanged(deltaStack);
        }
        
    }

    /// <summary>
    /// Updates the cooldown to add stacks of the stack component. To be subscribed to a listener.
    /// </summary>
    /// <param name="tick">deltaTime that has transocurred</param>
    public void UpdateCDToAdd(object sender, TickEventArgs tick)
    {   
        _currentCDToAdd = _currentCDToAdd - tick.value < 0 ? 0 : _currentCDToAdd - tick.value; 
        
        if (_currentCDToAdd == 0)
            GameManager.Instance.tickHandler -= UpdateCDToAdd;
    }
}