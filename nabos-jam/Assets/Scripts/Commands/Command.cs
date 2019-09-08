using UnityEngine;

/// <summary>
/// <c> Command </c> pattern base class.
/// </summary>
public abstract class Command 
{
    /// <summary>
    /// Exectutes the command behaviour.
    /// </summary>
    public abstract void Do();

    /// <summary>
    /// Undp the changes done when executing this command in the Do method.
    /// </summary>
    public abstract void Undo();
}