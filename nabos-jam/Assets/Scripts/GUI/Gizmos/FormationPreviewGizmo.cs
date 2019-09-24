using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class <c> MoveInFormationGUI </c> handles the gizmos that show the preview of the formation of the active units when getting the movement input.
/// </summary>
public class FormationPreviewGizmo : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _unitGizmos = new GameObject[0];

    /// <summary>
    /// Sets the positions and rotations of the gizmos given a list of positions and rotation properly ordered and Activates;
    /// </summary>
    /// <param name="positions">List of all desired units positions.</param>
    /// <param name="rotations">List of all desired units rotations.</param>
    public void Show(List<Vector3> positions, List<Quaternion> rotations)
    {
        // Init the transforms
        for (int i = 0; i < positions.Count; i++)
        {
            _unitGizmos[i].transform.position = positions[i];
            _unitGizmos[i].transform.rotation = rotations[i];
        }

        // Activate only used gizmos
        for (int i = 0; i < GameManager.Instance.PlayerParty.ActiveUnits.Count; i++)
            _unitGizmos[i].SetActive(true);
    }

    /// <summary>
    /// Hides the gizmos
    /// </summary>
    public void Hide() 
    {
        foreach (GameObject gizmo in _unitGizmos)
        {
            gizmo.SetActive(false);
        }  
    }
}