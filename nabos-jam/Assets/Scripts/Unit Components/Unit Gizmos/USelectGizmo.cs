using UnityEngine;
using System;

/// <summary>
/// Unit component that handles the selection gizmo of the unit.
/// </summary>
[Serializable]
public class USelectGizmo
{
    [SerializeField]
    SelectGizmoSetting materialSettings = null;

    [SerializeField]
    private MeshRenderer _selectGizmoRenderer = null;

    //////////////// METHODS ////////////////

    /// <summary>
    /// Turns on and sets the type of the highlight of the gizmo. 
    /// </summary>
    /// <param name="intensity">Type of the highlifht.</param>
    public void SetIntensity(UnitGizmoIntensity intensity)
    {
        _selectGizmoRenderer.enabled = true;
        switch (intensity)
        {
            case (UnitGizmoIntensity.High):
                _selectGizmoRenderer.material = materialSettings.hightIntensityMat;
            break;
            case (UnitGizmoIntensity.Medium):
                _selectGizmoRenderer.material = materialSettings.mediumIntensityMat;
            break;
            case (UnitGizmoIntensity.Low):
                _selectGizmoRenderer.material = materialSettings.lowIntensityMat;
            break;
        }
    }

    /// <summary>
    ///  Turn off the gizmo.
    /// </summary>
    public void Disable()
    {
        _selectGizmoRenderer.enabled = false;
    }

}