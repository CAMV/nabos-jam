using UnityEngine;

[CreateAssetMenu(menuName = "Input Settings/Move Input Setting")]
public class MoveIA : InputAction
{
    [SerializeField]
    private int _terrainLayer;
    
    public override Command GetInputCommand()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
        int layerMask = 1 << _terrainLayer; 
        RaycastHit hit;

        Debug.DrawRay(r.origin, r.direction*RAYCAST_LENGTH, Color.red, 5f);

        if (Physics.Raycast(r, out hit, RAYCAST_LENGTH, layerMask))
            return new MoveCmd(
                        GameManager.Instance.PlayerSquad.ActiveUnit, 
                        hit.point
                    );

        return null;
    }
}