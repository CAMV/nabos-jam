using UnityEngine;
using System.Collections.Generic;

public class SelectCmd : Command
{
    private List<Unit> _newActiveUnits, _oldActiveUnits;

    public SelectCmd(List<Unit> units)
    {
        _newActiveUnits = units;   
    }

    override public void Do()
    {
        _oldActiveUnits = GameManager.Instance.PlayerSquad.ActiveUnits;
        GameManager.Instance.PlayerSquad.ActiveUnits = _newActiveUnits;
    }


    override public void Undo()
    {
        
    } 
}