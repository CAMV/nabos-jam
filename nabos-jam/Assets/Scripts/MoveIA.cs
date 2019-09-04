using UnityEngine;

[CreateAssetMenu(menuName = "Input Settings/Move Input Setting")]
public class MoveIA : InputAction
{
    [SerializeField]
    private int _terrainLayer;

    private const float RAYCAST_LENGTH = 100;
    

    public override Command GetInputCommand(Unit u)
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
            int layerMask = 1 << _terrainLayer; 
            RaycastHit hit;

            Debug.DrawRay(r.origin, r.direction*RAYCAST_LENGTH, Color.red, 5f);

            if (Physics.Raycast(r, out hit, RAYCAST_LENGTH, layerMask))
                return new MoveCmd(u, hit.point);

            return null;
    }

}