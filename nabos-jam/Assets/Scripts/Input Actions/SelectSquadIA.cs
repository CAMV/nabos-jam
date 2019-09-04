using UnityEngine;

[CreateAssetMenu(menuName = "Input Settings/Select Squad Input Setting")]
public class SelectSquadIA : InputAction
{

    public override Command GetInputCommand()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;

        Debug.DrawRay(r.origin, r.direction*RAYCAST_LENGTH, Color.red, 5f);

        foreach(var unit in GameManager.Instance.PlayerSquad.Members)
        {
            if (unit.SelectCollider && unit.SelectCollider.Raycast(r, out hit, RAYCAST_LENGTH))
            {
                return new SelectCmd(unit);        
            }
        }

        return null;
    }
}