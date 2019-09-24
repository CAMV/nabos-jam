using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Singleton that manages the game states as well as access to diferent key strucutures
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private PartyController _playerParty = null;

    [SerializeField]
    private List<FormationAction> _formationsAvailable = new List<FormationAction>();

    public event EventHandler<TickEventArgs> tickHandler;
    
    private const float TICK_TIME = .200f;

    //////////////// PROPERTIES ////////////////

    /// <summary>
    /// Player's party controller
    /// </summary>
    public PartyController PlayerParty {
        get {
            return _playerParty;
        }
    }

    /// <summary>
    /// Get a list of the formation availables to the player
    /// </summary>
    public List<FormationAction> FormationAvailable {
        get {
            return _formationsAvailable;
        }
    }

    //////////////// METHODS ////////////////
    
    /// <summary>
    /// Send tick message to all subcribed events
    /// </summary>
    /// <param name="tickValue">deltaTime between each tick.</param>
    private IEnumerator Tick(float tickValue)
    {
        float time = Time.time;
        
        EventHandler<TickEventArgs> handler = tickHandler;

        while (true)
        {
            time = Time.time - time;
            handler = tickHandler;
            if (handler != null)
            {
                handler(this, new TickEventArgs(time));
            }

            yield return new WaitForSeconds(tickValue);
        }
    }

    void Start()
    {
        StartCoroutine(Tick(TICK_TIME));
    }


}