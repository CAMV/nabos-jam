using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Input Settings/Move Input Setting")]
public class MoveIA : InputAction
{
    [SerializeField]
    private int _terrainLayer;
    
    public override IEnumerator ExecuteAction()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
        int layerMask = 1 << _terrainLayer; 
        RaycastHit hit;

        Debug.DrawRay(r.origin, r.direction*RAYCAST_LENGTH, Color.red, 5f);

        if (Physics.Raycast(r, out hit, RAYCAST_LENGTH, layerMask))
        {
            foreach(Unit u in GameManager.Instance.PlayerSquad.ActiveUnits)
            {
                GameManager.Instance.PlayerSquad.AddCommand(new MoveCmd(u, hit.point));
            }
        }
        
        yield return null;
    }
}