using UnityEngine;

public class UGizmos : MonoBehaviour
{
    [SerializeField]
    protected USelectGizmo _selectGizmo = null;

    [SerializeField]
    private UVectorGizmo _vectorGizmo = null;

    [SerializeField]
    private UAreaGizmo _areaGizmo = null;

    //////////////// PROPERTIES ////////////////

    /// <summary>
    /// The select gizmo component of the unit, if it has it.
    /// </summary>
    public USelectGizmo SelectGizmo{
        get {
            return _selectGizmo;
        }
    }

    /// <summary>
    /// The vector gizmo component of the unit, if it has it.
    /// </summary>
    public UVectorGizmo VectorGizmo{
        get {
            return _vectorGizmo;
        }
    }

    /// <summary>
    /// The area gizmo component of the unit, if it has it.
    /// </summary>
    public UAreaGizmo AreaGizmo{
        get {
            return _areaGizmo;
        }
    }
}