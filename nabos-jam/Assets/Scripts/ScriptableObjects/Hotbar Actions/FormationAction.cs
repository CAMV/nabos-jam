using UnityEngine;

/// <summary>
/// Represents a formation command that can be assigned as a hotbar action.
/// </summary>
[CreateAssetMenu(menuName = "Hotbar/FormationAction")] 
public class FormationAction : ScriptableObject, IHotbarAction
{
    [SerializeField]
    private GUIData _guiData = null;

    [SerializeField]
    Formation _formation = null;

    /// <summary>
    /// Gets the formation's guiData 
    /// </summary>
    public GUIData GUIData {
        get {
            return _guiData;
        }
    }

    /// <summary>
    /// Fromations action have no cooldown so they return always 0 if its a valid one, or false is is not a valid one.
    /// </summary>
    /// <remarks>
    public float CurrentCooldown{
        get {
            return this.isActivable ? 0 : 1;
        }
    }

    /// <summary>
    /// Return true if the skill is exectable.null Ex. cooldown, stunts, or something.
    /// </summary>
    /// <returns>Is skill executable.</returns>s
    public bool isActivable {
        get {
            return _formation.Size >= GameManager.Instance.PlayerParty.Size;
        }
    }

    /// <summary>
    /// Exectue function to call when the HotbarSlot with this action is activated
    /// </summary>
    /// <param name="isQuickCast">Indicate if quickcasting is active.</param>
    public void Execute(bool isQuickCast) 
    {
        GameManager.Instance.PlayerParty.Formation = _formation;
    }
}