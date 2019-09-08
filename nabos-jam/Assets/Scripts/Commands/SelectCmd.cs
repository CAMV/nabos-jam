using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Command that tells the player's squad controller to set as activate units a list of units
/// </summary>
public class SelectCmd : Command
{
    private List<Unit> _newActiveUnits, _oldActiveUnits;

    /// <summary>
    /// Constructs the command given a list of units.
    /// </summary>
    /// <param name="units">Units to be assigned as active units.</param>
    public SelectCmd(List<Unit> units)
    {
        _newActiveUnits = units;   
    }

    /// <summary>
    /// Set the player's squad controller active units with its units.
    /// </summary>
    override public void Do()
    {
        _oldActiveUnits = GameManager.Instance.PlayerSquad.ActiveUnits;
        GameManager.Instance.PlayerSquad.ActiveUnits = _newActiveUnits;
    }

    /// <summary>
    /// Undo any movement done by this command. DOES NOTHING!!!
    /// </summary>
    override public void Undo()
    {
        
    } 
}