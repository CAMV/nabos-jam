using UnityEngine;

/// <summary>
/// Class <c> Formation </c> models a formation of a set of units, based on the position of a non-specific leader unit.
/// </summary>
[CreateAssetMenu(menuName = "Formation")]
public class Formation : ScriptableObject
{
    // Number of units the formation can hold.
    [SerializeField]
    private int _size = 1;
    // xyz components are the position offset from the leader
    // w is the angle y offset from the leader;
    [SerializeField]
    private Vector4[] _fTransform = new Vector4[1];

    //////////////// PROPERTIES ////////////////

    /// <summary>
    /// Maximun number of units the formation can hold
    /// </summary>
    public int Size{
        get {
            return _size;
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

    //////////////// METHODS ////////////////

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
}