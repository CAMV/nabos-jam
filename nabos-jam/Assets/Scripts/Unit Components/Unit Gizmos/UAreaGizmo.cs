using UnityEngine;

/// <summary>
/// Class that handles the area gizmo
/// </summary>
[RequireComponent(typeof(SkinnedMeshRenderer))]
public class UAreaGizmo : MonoBehaviour
{
    SkinnedMeshRenderer _myRender;

    public void OnAwake()
    {
        _myRender = GetComponent<SkinnedMeshRenderer>();
    }

    /// <summary>
    /// Given a position and the radius of the area, display the skill area gizmo.
    /// </summary>
    /// <param name="position">Position for the gizmo.</param>
    /// <param name="radius">Radius for the area.</param>
    public void Show(Vector3 position, float radius)
    {
        if (radius < 1 && radius > 10)
            return;

        if (!_myRender)
            _myRender = GetComponent<SkinnedMeshRenderer>();

        _myRender.SetBlendShapeWeight(0, radius*10);

        transform.position = position;
    }

    /// <summary>
    /// Deactivates the gizmo
    /// </summary>
    public void Hide()
    {
        if (!_myRender)
            _myRender = GetComponent<SkinnedMeshRenderer>();

        _myRender.enabled = false;
    }


}