using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class <c> MoveInFormationGUI </c> handles the gizmos that show the preview of the formation of the active units when getting the movement input.
/// </summary>
public class MoveInFormationGUI : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _unitGizmos = new GameObject[0];

    /// <summary>
    /// Activates or deactivates the gizmosm depending of the given boolean.
    /// </summary>
    /// <param name="active">set gizmo active?</param>
    public void SetActive(bool active)
    {
        if (active)
        {
            for (int i = 0; i < GameManager.Instance.PlayerParty.ActiveUnits.Count; i++)
            {   
            _unitGizmos[i].SetActive(true);
            }           
        }
        else
        {
            foreach (GameObject gizmo in _unitGizmos)
            {
                gizmo.SetActive(false);
            }         
        }
    }

    /// <summary>
    /// Sets the positions and rotations of the gizmos given a list of positions and rotation properly ordered.
    /// </summary>
    /// <param name="positions">List of all desired units positions.</param>
    /// <param name="rotations">List of all desired units rotations.</param>
    public void SetValues(List<Vector3> positions, List<Quaternion> rotations)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            _unitGizmos[i].transform.position = positions[i];
            _unitGizmos[i].transform.rotation = rotations[i];
        }
    }

}