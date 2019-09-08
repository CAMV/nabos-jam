using UnityEngine;

/// <summary>
/// Class to define a null <c> Formation </c>. NoT USE CURRENTLY
/// </summary>
public class NullFormation 
{

    /// <summary>
    /// dummy indexer
    /// </summary>
    /// <value></value>
    public Vector4 this[int index]
    {
        get {
            return Vector4.zero;
        }
        set {
            // :D
        }
    }

    /// <summary>
    /// "You are a big number"
    ///  "For you..."
    /// </summary>
    public int Size{
        get{
            return int.MaxValue;
        }
    }

    /// <summary>
    /// Returns a null position (0,0,0).
    /// </summary>
    /// <param name="index">Whatever...</param>
    /// <returns>Vector3 full of zer000s</returns>
    public Vector3 GetPosOffset(int index)
    {
        return Vector3.zero;
    }
    

    /// <summary>
    /// Returns the null y-rotation 0.
    /// </summary>
    /// <param name="index">Who cares?</param>
    /// <returns>Z-E-R-O</returns>
    public float GetEAOffset(int index)
    {
        return 0.0f;
    }



}