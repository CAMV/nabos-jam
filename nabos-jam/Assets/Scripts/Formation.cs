using UnityEngine;


[CreateAssetMenu(menuName = "Formation")]
public class Formation : ScriptableObject
{
    [SerializeField]
    private int _size = 1;
    // xyz components are the position offset from the leader
    // w is the angle y offset from the leader;
    [SerializeField]
    private Vector4[] _fTransform = new Vector4[1];

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

    public static Quaternion GetFollowerRotation(Formation f, int index, Matrix4x4 leaderMatrix)
    {
        return  leaderMatrix.rotation * Quaternion.Euler(0, f._fTransform[index].w, 0);
    }

    public static Vector3 GetFollowerPosition(Formation f, int index, Matrix4x4 leaderMatrix)
    {
        return  leaderMatrix.MultiplyPoint(new Vector3 (
                                                f._fTransform[index].x, 
                                                f._fTransform[index].y, 
                                                f._fTransform[index].z)
                                            );
    }

}