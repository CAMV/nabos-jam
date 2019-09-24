using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class UVectorGizmo : MonoBehaviour 
{
    private float _maxValue = 10;

    SkinnedMeshRenderer _myRender;

    /// <summary>
    /// Set the maximun value the vector can have
    /// </summary>
    /// <value></value>
    public float MaxValue {
        set {
            if (value <= 10 && value > 1)
                _maxValue = value; 
        }
    }

    public void OnAwake()
    {
        _myRender = GetComponent<SkinnedMeshRenderer>();
    }

    /// <summary>
    /// Sets the length of the vector gizmos and activates it
    /// </summary>
    /// <param name="target">Target to use for vector lenght</param>
    public void Show(Vector3 target, float maxValue)
    {
        _maxValue = maxValue;

        transform.LookAt(target);   


        float distance = Vector3.Distance(transform.position, target);
        if (distance > _maxValue)
            distance = _maxValue;

        if (!_myRender)
            _myRender = GetComponent<SkinnedMeshRenderer>();

        _myRender.SetBlendShapeWeight(0, Mathf.Max(1, distance)*10);

        _myRender.enabled = true;
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