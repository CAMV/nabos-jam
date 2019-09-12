using UnityEngine;

/// <summary>
/// Interface to make a object executable by the hotbar
/// </summary>
public interface IHotbarAction
{
    /// <summary>
    /// Exectue function to call when the HotbarSlot with this action is activated
    /// </summary>
    /// <param name="isQuickCast">Indicate if quickcasting is active.</param>
    void Execute(bool isQuickCast);

    /// <summary>
    /// Gets the icon to be display in the hotbar
    /// </summary>
    /// <value>GUI Icon of the Skill</value>
    Sprite Icon {
        get;
    }

    /// <summary>
    /// Gets the cooldown remaining where 0 is ready to active adn 1 is full cooldown.
    /// </summary>
    /// <remarks>
    /// Used for GUI purposes.
    /// </remarks>
    /// <value>Cooldown of remaining between [0-1].null </value>
    float CurrentCooldown{
        get;
    }

    /// <summary>
    /// Return true if the skill is exectable.null Ex. cooldown, stunts, or something.
    /// </summary>
    /// <returns>Is skill executable.</returns>
    bool isActivable {
        get;
    }

    /// <summary>
    /// Gets the name of the action.
    /// </summary>
    string Name { 
        get;
    }

    /// <summary>
    /// Compare currentaction with a given one
    /// </summary>
    /// <param name="action">action to compare with</param>
    /// <returns></returns>
    bool Compare (IHotbarAction action);


}