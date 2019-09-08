using UnityEngine;

/// <summary>
/// Unit component that handles the selection gizmo of the unit.
/// </summary>
[RequireComponent(typeof(MeshRenderer))]
public class USelectGizmo : MonoBehaviour
{
    [SerializeField]
    SelectGizmoSetting materialSettings = null;

    private Material _hMaterial, _mMaterial, _lMaterial;

    /// <summary>
    /// Type of a unit's selection gizmo
    /// </summary>
    public enum SelectGizmoIntensity
    {
        High, Medium, Low
    }

    private MeshRenderer _myMR;

    void Awake()
    {
        _hMaterial = materialSettings.hightIntensityMat;
        _mMaterial = materialSettings.mediumIntensityMat;
        _lMaterial = materialSettings.lowIntensityMat;

        _myMR = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// Turns on and sets the type of the highlight of the gizmo. 
    /// </summary>
    /// <param name="intensity">Type of the highlifht.</param>
    public void SetIntensity(SelectGizmoIntensity intensity)
    {
        _myMR.enabled = true;
        switch (intensity)
        {
            case (SelectGizmoIntensity.High):
                _myMR.material = _hMaterial;
            break;
            case (SelectGizmoIntensity.Medium):
                _myMR.material = _mMaterial;
            break;
            case (SelectGizmoIntensity.Low):
                _myMR.material = _lMaterial;
            break;
        }
    }

    /// <summary>
    ///  Turn off the gizmo.
    /// </summary>
    public void Disable()
    {
        _myMR.enabled = false;
    }

}