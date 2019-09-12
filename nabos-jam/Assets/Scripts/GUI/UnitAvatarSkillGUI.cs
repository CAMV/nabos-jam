using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
///  Class <c>UnitAvatarSkill</c> GUI model the omponenet that handles Avatar skills GUI
/// </summary>
public class UnitAvatarSkillGUI : MonoBehaviour
{
    [SerializeField]
    private Image _expandTrigger;
    [SerializeField]
    private HotbarSlotGUI[] _skillSlotGUI = new HotbarSlotGUI[4];

    private bool _isExpanded = false;

    /// <summary>
    /// Indexer to set the skills in the GUI
    /// </summary>
    public Skill this[int index]
    {
        get {
            return _skillSlotGUI[index].HotbarAction as Skill;
        }
        set {
            _skillSlotGUI[index].Initialize(value);
        }
    }

    /// <summary>
    /// Gets the number of GUI Slots the Skill GUI has
    /// </summary>
    public int Size {
        get {
            return _skillSlotGUI.Length;
        }
    }

}