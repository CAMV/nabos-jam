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
    public void SetIntensity(UnitGizmoIntensity intensity)
    {
        _myMR.enabled = true;
        switch (intensity)
        {
            case (UnitGizmoIntensity.High):
                _myMR.material = _hMaterial;
            break;
            case (UnitGizmoIntensity.Medium):
                _myMR.material = _mMaterial;
            break;
            case (UnitGizmoIntensity.Low):
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