using UnityEngine;
using System;

/// <summary>
/// Class that handles the area gizmo
/// </summary>
[Serializable]
public class UAreaGizmo
{
    [SerializeField]
    SkinnedMeshRenderer _areaGizmoRender = null;

    //////////////// METHODS ////////////////

    /// <summary>
    /// Given a position and the radius of the area, display the skill area gizmo.
    /// </summary>
    /// <param name="position">Position for the gizmo.</param>
    /// <param name="radius">Radius for the area.</param>
    public void Show(Vector3 position, float radius)
    {
        if (radius < 1 && radius > 10)
            return;

        _areaGizmoRender.SetBlendShapeWeight(0, radius*10);

        _areaGizmoRender.transform.position = position;
    }

    /// <summary>
    /// Deactivates the gizmo
    /// </summary>
    public void Hide()
    {
        _areaGizmoRender.enabled = false;
    }
}