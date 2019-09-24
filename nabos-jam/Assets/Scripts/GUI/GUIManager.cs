using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
/// Singleton <c> GUI Manager </c> models all GUI components, giving access to all of them. All GUI components must be children of this class as well as all UI canvases.
/// </summary>
[RequireComponent(typeof(EventSystem))]
public class GUIManager : Singleton<GUIManager>
{

    [SerializeField]
    private ScreenSelectionGizmo _screenSelectionGizmo = null;
    [SerializeField]
    private FormationPreviewGizmo _fPreviewGizmo = null;
    [SerializeField]
    private MovementFeedbackGizmo _mFeedbackGizmo = null;
    [SerializeField]
    private PartyAvatarsHandler _partyAvatarsH = null;
    [SerializeField]
    private FormationHotbar _formationHotbar = null;
    [SerializeField]
    private Hotbar _hotbar = null;

    [SerializeField]
    private HotbarSlot _dummySlot = null;

    private GraphicRaycaster[] _graphicRCasters;
    private EventSystem _eventSystem;

    /// <summary>
    /// Component that handles formation preview when holding Move Input Button.
    /// </summary>
    /// <value> MoveInFormationGUI component.</value>
    public FormationPreviewGizmo FormationPreviewGizmo
    {
        get {
            return _fPreviewGizmo;
        }
    } 

    /// <summary>
    /// Component that handles movement feedback when moving.
    /// </summary>
    /// <value>MoveTargetGUI component.</value>
    public MovementFeedbackGizmo MovementFeedbackGizmo
    {
        get {
            return _mFeedbackGizmo;
        }
    }

    /// <summary>
    /// Component that handles party units avatars GUI.
    /// </summary>
    /// <value>PartyUnitsGUI component.</value>
    public PartyAvatarsHandler PlayerAvatarsHandler
    {
        get {
            return _partyAvatarsH;
        }
    }

    /// <summary>
    /// Component that handles selection area GUI.
    /// </summary>
    /// <value>SelectSquareGUI component.</value>
    public ScreenSelectionGizmo ScreenSelectionGizmo {
        get {
            return _screenSelectionGizmo;
        }
    }

    /// <summary>
    /// Component that handles party formation selection GUI.
    /// </summary>
    /// <value>SelectSquareGUI component.</value>
    public FormationHotbar FormationHotbar {
        get {
            return _formationHotbar;
        }
    }

    /// <summary>
    /// Returns the hotbar GUI component
    /// </summary>
    public Hotbar Hotbar {
        get {
            return _hotbar;
        }
    }

    /// <summary>
    /// Returns all the graphics raycaster of the scene
    /// </summary>
    public GraphicRaycaster[] GraphicsRCasters {
        get {
            return _graphicRCasters;
        }
    }

    /// <summary>
    /// Returns true if the mouse is over any GUI element that is children of GUIManager
    /// </summary>
    public bool IsMouseOverGUI
    {
        get {
        
            PointerEventData eventData = new PointerEventData(_eventSystem);
            eventData.position = Input.mousePosition;
            List<RaycastResult> hits = new List<RaycastResult>();

            foreach (var rayCaster in _graphicRCasters)
            {
                hits.Clear();
                rayCaster.Raycast(eventData, hits);
                
                if (hits.Count > 0)
                    return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Given a hotbarAction, returns a dummy hotbarSlot to use for draggin visualization purposes.
    /// </summary>
    /// <param name="hAction">Hotbar action to be held by the dummy.</param>
    /// <returns>HotbarSlotGUI to be dragged around.</returns>
    public HotbarSlot GetDummySlot(IHotbarAction hAction)
    {
        _dummySlot.Initialize(hAction);
        return _dummySlot;
    }

    override protected void OnAwake()
    {
        _eventSystem = GetComponent<EventSystem>();
        _graphicRCasters = GetComponentsInChildren<GraphicRaycaster>();
    }
}