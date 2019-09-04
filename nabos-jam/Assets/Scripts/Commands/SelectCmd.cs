using UnityEngine;

public class SelectCmd : Command
{
    private Unit _newActiveUnit, _oldActiveUnit;

    public SelectCmd(Unit u)
    {
        _newActiveUnit = u;   
    }


    override public void Do()
    {
        _oldActiveUnit = GameManager.Instance.PlayerSquad.ActiveUnit;
        GameManager.Instance.PlayerSquad.ActiveUnit = _newActiveUnit;
    }


    override public void Undo()
    {
        
    } 
}