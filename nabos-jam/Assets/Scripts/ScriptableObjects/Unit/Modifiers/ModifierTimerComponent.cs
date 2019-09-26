using UnityEngine;
using System;

[Serializable]
public class ModifierTimerComponent
{
   
    [SerializeField]
    private float _duration = 0;
    
    private float _currentDuration;
    private Func<bool> _OnTimerExpired;

    //////////////// METHODS ////////////////

    /// <summary>
    /// Intialize the timer component with a given tick handler and Function to run when the timer is expired.
    /// </summary>
    /// <param name="OnTimerExpired">The function to call when the timer expires.</param>
    public void Inititalize(Func<bool> OnTimerExpired)
    {
        _OnTimerExpired = OnTimerExpired;

        StartTimer();
    }

    /// <summary>
    /// Starts the timer
    /// </summary>
    public void StartTimer()
    {
        if (_duration <= 0)
            return;

        _currentDuration = _duration;
        GameManager.Instance.tickHandler += UpdateCurrentDuration;
    }

    /// <summary>
    /// Updates the timer of the life of the modifier. To be subscribed to a tick handler.
    /// </summary>
    /// <param name="tick">deltaTime that has transocurred</param>
    public void UpdateCurrentDuration(object sender, TickEventArgs tick)
    {   
        _currentDuration = _currentDuration - tick.value < 0 ? 0 : _currentDuration - tick.value; 

        if (_currentDuration == 0)
        {
            _OnTimerExpired();
            GameManager.Instance.tickHandler -= UpdateCurrentDuration;
        }
    }
    
}