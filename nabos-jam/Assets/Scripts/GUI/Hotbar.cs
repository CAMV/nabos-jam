using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
/// Class <c> Hotbar </c> handles the hotbar of the player to set hotkeys for actions.
/// </summary>
public class Hotbar : MonoBehaviour, IPointerClickHandler
{
    private const int N_OF_SLOTS = 8;

    [SerializeField]
    private List<HotbarSlotGUI> _slots = new List<HotbarSlotGUI>(N_OF_SLOTS);

    [SerializeField]
    private HotbarSlotGUI _dummySlot = null;

    /// <summary>
    /// Indexer to handle the slots as an array of Skills
    /// </summary>
    /// <value></value>
    public IHotbarAction this[int index] {
        get {
            return _slots[index].HotbarAction;
        }

        set {
            _slots[index].Initialize(value);
        }
    }

    /// <summary>
    /// Returns the number of slots in the Hotbar.
    /// </summary>
    public int Size { 
        get {
            return _slots.Count;
        }
    }

    /// <summary>
    /// Given a hotbarAction, returns a dummy hotbarSlot to use for draggin visualization purposes.
    /// </summary>
    /// <param name="hAction">Hotbar action to be held by the dummy.</param>
    /// <returns>HotbarSlotGUI to be dragged around.</returns>
    public HotbarSlotGUI GetDummySlot(IHotbarAction hAction)
    {
        _dummySlot.Initialize(hAction);
        return _dummySlot;
    }

    /// <summary>
    /// Returns true if a given HotbarAction is already present in te hotbar.
    /// </summary>
    /// <param name="hAction">Action to find in the hotbar.</param>
    public bool IsActionInHotbar(IHotbarAction hAction) 
    {
        foreach (var slot in _slots)
        {
            if (slot.HotbarAction == hAction)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Return number of the slot in which a given action is located in the hotbar.true If the action is not in the hotbar, return -1;
    /// </summary>
    /// <param name="hAction">Action to look index for.</param>
    public int indexOfAction(IHotbarAction hAction)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].HotbarAction == hAction)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Initialize a given slot position with a given hotbar action.
    /// </summary>
    /// <param name="slotIndex">Slot index to initialize.</param>
    /// <param name="action">Action to use initializa.</param>
    public void InitializeSlot(int slotIndex, IHotbarAction action)
    {
        if (slotIndex >= 0 && slotIndex < _slots.Count)
        {
            _slots[slotIndex].Initialize(action);
        }
    }

    /// <summary>
    /// /// Exectes the hotbar slot action clicked
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        HotbarSlotGUI slotClicked =  eventData.rawPointerPress.GetComponentInParent<HotbarSlotGUI>();

        if (!slotClicked)
            return;

        if (_slots.Contains(slotClicked))
            slotClicked.ExecuteSlotAction();
    }
}