using UnityEngine;


[CreateAssetMenu(menuName = "Formation")]
public class Formation : ScriptableObject
{
    [SerializeField]
    private int _size;
    // xyz components are the position offset from the leader
    // w is the angle y offset from the leader;
    [SerializeField]
    private Vector4[] _fTransform;

    public int Size{
        get {
            return _size;
        }
    }

    public Vector3 GetPosOffset(int index)
    {
        if (index >= 0 && index < _size)
            return new Vector3(_fTransform[index].x, _fTransform[index].y, _fTransform[index].z);

        return Vector3.zero;
    }

    public float GetEAOffset(int index)
    {
        if (index > 0 && index < _size)
            return _fTransform[index].w;

        return -1;
    }
}