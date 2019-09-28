using UnityEngine;
using System;

[Serializable]
public class UVectorGizmo 
{
    
    [SerializeField]
    protected SkinnedMeshRenderer _vectorGizmoRender;

    private float _maxValue = 10;
    
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

    //////////////// METHODS ////////////////

    /// <summary>
    /// Sets the length of the vector gizmos and activates it
    /// </summary>
    /// <param name="target">Target to use for vector lenght</param>
    public void Show(Vector3 target, float maxValue)
    {
        _maxValue = maxValue;

        _vectorGizmoRender.transform.LookAt(target);   


        float distance = Vector3.Distance(_vectorGizmoRender.transform.position, target);
        if (distance > _maxValue)
            distance = _maxValue;


        _vectorGizmoRender.SetBlendShapeWeight(0, Mathf.Max(1, distance)*10);

        _vectorGizmoRender.enabled = true;
    }

    /// <summary>
    /// Deactivates the gizmo
    /// </summary>
    public void Hide()
    {
        _vectorGizmoRender.enabled = false;
    }
}