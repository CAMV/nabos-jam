using UnityEngine;

/// <summary>
/// Interface to handle drag and drop behaviour in the GUI. 
/// </summary>
/// <typeparam name="T">Object Dragged Type</typeparam>
public interface IDragComponent<T>
{
    /// <summary>
    /// Starts the dragging of a given object.
    /// </summary>
    /// <param name="elementToDrag">Object that is gonna be dragged</param>
    void StartDrag(T elementToDrag);

    /// <summary>
    /// Updates the dragged object position based in a Vector2 input.
    /// </summary>
    /// <param name="mousePos">Input value to update the dragged object position.</param>
    void UpdateDrag(Vector2 mousePos);

    /// <summary>
    /// Stops the dragging of the current dragged object.
    /// </summary>
    void StopDrag();
}  
