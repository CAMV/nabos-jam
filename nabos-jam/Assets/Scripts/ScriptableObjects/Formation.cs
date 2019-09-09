using UnityEngine;

/// <summary>
/// Class <c> Formation </c> models a formation of a set of units, based on the position of a non-specific leader unit.
/// </summary>
[CreateAssetMenu(menuName = "Formation")]
public class Formation : ScriptableObject
{
    [SerializeField]
    private Sprite _icon = null;
    // Number of units the formation can hold.
    [SerializeField]
    private int _size = 1;
    // xyz components are the position offset from the leader
    // w is the angle y offset from the leader;
    [SerializeField]
    private Vector4[] _fTransform = new Vector4[1];

    /// <summary>
    /// Maximun number of units the formation can hold
    /// </summary>
    public int Size{
        get {
            return _size;
        }
    }

    /// <summary>
    /// Icon of the formation for GUI purposes.
    /// </summary>
    public Sprite Icon {
        get {
            return _icon;
        }
    }
    
    /// <summary>
    /// Indexer for formation that gets you the vector4 where xyz is the position and the w is the y-rotation, both in local space
    /// </summary>
    /// <value></value>
    public Vector4 this[int index]
    {
        get {
            return _fTransform[index];
        }
        set {
            _fTransform[index] = value;
        }
    }

    /// <summary>
    /// Returns the rotation in world space of a follower, given the leader transform matrix.
    /// </summary>
    /// <param name="formation">Formation to use to calculate the rotation</param>
    /// <param name="index">Position of the follower in the formation, where 0 is the first follower</param>
    /// <param name="leaderMatrix">Transform matrix of the leader</param>
    /// <returns>Quaternion with the world space rotation of the follower</returns>
    public static Quaternion GetFollowerRotation(Formation formation, int index, Matrix4x4 leaderMatrix)
    {
        return  leaderMatrix.rotation * Quaternion.Euler(0, formation[index].w, 0);
    }

    /// <summary>
    /// Returns the position in world space of a follower, given the leader transform matrix.
    /// </summary>
    /// <param name="formation">Formation to use to calculate the rotation</param>
    /// <param name="index">Position of the follower in the formation, where 0 is the first follower</param>
    /// <param name="leaderMatrix">Transform matrix of the leader</param>
    /// <returns>Vector3 with the wolrd space position of the follower</returns>
    public static Vector3 GetFollowerPosition(Formation formation, int index, Matrix4x4 leaderMatrix)
    {
        return  leaderMatrix.MultiplyPoint(new Vector3 (
                                                formation[index].x, 
                                                formation[index].y, 
                                                formation[index].z)
                                            );
    }

    //

    /// <summary>
    /// Returns the position of a follower in local space position, with the leader at (0,0,0).
    /// </summary>
    /// <param name="index">Position of the follower in the formation, where 0 is the first follower.</param>
    /// <returns>Vector3 with position of the follower in local space</returns>
    public Vector3 GetPosOffset(int index)
    {
        if (index >= 0 && index < _size)
            return new Vector3(_fTransform[index].x, _fTransform[index].y, _fTransform[index].z);

        return Vector3.zero;
    }

    /// <summary>
    /// Returns the y-rotation angle of a follower in local space rotation, with the leader of the formation with 0.
    /// </summary>
    /// <param name="index">Position of the follower in the formation, where 0 is the first follower.</param>
    /// <returns>Y-rotation angle of the follower in local space</returns>
    public float GetEAOffset(int index)
    {
        if (index > 0 && index < _size)
            return _fTransform[index].w;

        return -1;
    }
}