using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Class <c> HotbarSlotGUI </c> models the GUI aspects of a hotbar slot.
/// </summary>
public class HotbarSlotGUI : MonoBehaviour, IDropHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField]
    private Image _iconRenderer = null;
    [SerializeField]
    private Image _cooldownIndicator = null;

    private IHotbarAction _myHotbarAction = null;
    private GameObject _dummyReference = null;

    /// <summary>
    /// Gets the action holded by the slot
    /// </summary>
    public IHotbarAction HotbarAction {
        get {
            return _myHotbarAction;
        }
    }

    /// <summary>
    /// Initialize the slot with an Action.
    /// </summary>
    /// <param name="action">Action to associate to the slot</param>
    public void Initialize(IHotbarAction action)
    {
        if (action != null)
        {
            _myHotbarAction = action;
            _iconRenderer.sprite = action.Icon;
            _iconRenderer.color = new Color(_iconRenderer.color.r, _iconRenderer.color.g, _iconRenderer.color.b, 1);
            _cooldownIndicator.fillAmount = action.CurrentCooldown;
        }
        else
        {
            _myHotbarAction = null;
            _iconRenderer.sprite = null;
            _iconRenderer.color = new Color(_iconRenderer.color.r, _iconRenderer.color.g, _iconRenderer.color.b, 0);
            _cooldownIndicator.fillAmount = 0;
        }
    }


    /// <summary>
    /// Executes the action associated with the slot.
    /// </summary>
    public void ExecuteSlotAction()
    {
        if (_myHotbarAction != null)
            _myHotbarAction.Execute(false || InputController.isAlwaysQuickCast);
    }

    /// <summary>
    /// Starts the drag of a hotbar slot element
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GUIManager.Instance.Hotbar)
        {
            Debug.Log("No hotbar available!");
            return;
        }

        if (_myHotbarAction == null)
            return;

        _dummyReference = GUIManager.Instance.Hotbar.GetDummySlot(_myHotbarAction).gameObject;
        _dummyReference.SetActive(true);
    }

    /// <summary>
    /// Activates the dummySlot when the dragginStarts
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (_dummyReference)
            _dummyReference.transform.position = eventData.position;
    }

    /// <summary>
    /// Deactivates the dummySlot when draggin stops
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_dummyReference)
            _dummyReference.SetActive(false);
    }

    /// <summary>
    /// Updates the slot if another slot with a different action is dropped on it
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        
        if (!GUIManager.Instance.Hotbar)
        {
            Debug.Log("No Hotbar GUI!");
            return;            
        }

        HotbarSlotGUI recieverSlot = eventData.pointerEnter.GetComponentInParent<HotbarSlotGUI>();
        HotbarSlotGUI senderSlot = eventData.pointerDrag.GetComponentInParent<HotbarSlotGUI>();

        if (!recieverSlot)
            return;

        if (recieverSlot == senderSlot)
            return;

        if (!recieverSlot.GetComponentInParent<Hotbar>())
            return;
            
        // Save old action of slot reciever in case we are swapping action
        IHotbarAction auxAction = senderSlot.HotbarAction;

        // Check if we are swapping slots
        if (GUIManager.Instance.Hotbar.IsActionInHotbar(senderSlot.HotbarAction))
        {
            GUIManager.Instance.Hotbar.InitializeSlot(
                                                GUIManager.Instance.Hotbar.indexOfAction(auxAction),
                                                recieverSlot.HotbarAction
                                            );
        }

        recieverSlot.Initialize(auxAction);
    }
}