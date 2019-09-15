using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controller that handles all the input actions available.
/// </summary>
public class InputController : MonoBehaviour
{
    public static bool isAlwaysQuickCast = false;

    [SerializeField]
    private List<InputAction> _inputActions = new List<InputAction>();

    private List<string> _blockedButtons = new List<string>();


    void Start()
    {
        StartCoroutine(UpdateCo());
    }

    /// <summary>
    /// Updates coroutine to make wait until a input action finish before exceuting another one. 
    /// </summary>
    private IEnumerator UpdateCo()
    {
        while (true)
        {
            foreach(var input in _inputActions)
            {
                if (input.CheckInput())
                {
                    bool blocked = false;
                    // Check if the button is blocked
                    int i;
                    for (i = 0; i < _blockedButtons.Count; i++)
                    {
                        if (_blockedButtons[i] == input.ButtonName)
                        {
                            blocked = true;
                            break;
                        }
                    }

                    if (!blocked) 
                        yield return StartCoroutine(input.ExecuteAction());  
                }

            }
            
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// Add a given key to the list of blocked input
    /// </summary>
    /// <param name="button">Name of the button</param>
    public void BlockButton(string button)
    {
        if (!_blockedButtons.Contains(button))
            _blockedButtons.Add(button);
    }

    /// <summary>
    /// Removed a given key to the list of blocked input
    /// </summary>
    /// <param name="button">Name of the button</param>
    public void UnlockButton(string button)
    {
        if (_blockedButtons.Contains(button))
            _blockedButtons.Remove(button);
    }

    /// <summary>
    /// Returns a given point  with the hit position of a raycast from the mouse to the terrain layer. If the raycast is successful, returns true and the hitpoitn is storied in a given vector. 
    /// </summary>
    /// <param name="point">Vector where the hitpoint is stored.</param>
    /// <returns>True if raycaste is succesful, false otherwise.</returns>
    public static bool GetTerrainRaycast(out Vector3 point) 
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << InputAction.TERRAIN_LAYER; 

        if (Physics.Raycast(r, out hit, InputAction.RAYCAST_LENGTH, layerMask) && hit.transform.tag == "Floor")
        {

            Debug.DrawLine(hit.point, hit.point + Vector3.up, Color.blue, .1f);
            Debug.DrawLine(hit.point, hit.point + Vector3.left, Color.blue, .1f);
            Debug.DrawLine(hit.point, hit.point + Vector3.right, Color.blue, .1f);
            Debug.DrawLine(hit.point, hit.point + Vector3.back, Color.blue, .1f);
            Debug.DrawLine(hit.point, hit.point + Vector3.forward, Color.blue, .1f);

            point = hit.point;

            return true;
        }

        point = Vector3.zero;

        return false;
    } 

}
