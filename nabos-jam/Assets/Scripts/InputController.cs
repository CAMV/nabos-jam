using UnityEngine;

public class InputController : Singleton<InputController>
{
    private const float RAYCAST_LENGTH = 100;
    private const int TERRAIN_LAYER = 8;

    public void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
            int layerMask = 1 << TERRAIN_LAYER; 
            RaycastHit hit;

            if (Physics.Raycast(r, out hit, RAYCAST_LENGTH, layerMask))
            {
                
            }
        }
        
    }
}