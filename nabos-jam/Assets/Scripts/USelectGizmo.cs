using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class USelectGizmo : MonoBehaviour
{
    [SerializeField]
    SelectGizmoSetting materialSettings = null;

    private Material _hMaterial, _mMaterial, _lMaterial;

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

    public void Disable()
    {
        _myMR.enabled = false;
    }

}