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
    private SelectSquareGUI _selectSqrGUI = null;
    [SerializeField]
    private MoveInFormationGUI _moveInFormGUI = null;
    [SerializeField]
    private MoveTargetGUI _moveTgGUI = null;
    [SerializeField]
    private SquadUnitsGUI _sqUnitsGUI = null;
    [SerializeField]
    private SquadFormationGUI _sqFormationGUI = null;
    [SerializeField]
    private Hotbar _hotbar = null;

    private GraphicRaycaster[] _graphicRCasters;
    private EventSystem _eventSystem;

    /// <summary>
    /// Component that handles formation preview when holding Move Input Button.
    /// </summary>
    /// <value> MoveInFormationGUI component.</value>
    public MoveInFormationGUI MoveInFormationGUI
    {
        get {
            return _moveInFormGUI;
        }
    } 

    /// <summary>
    /// Component that handles movement feedback when moving.
    /// </summary>
    /// <value>MoveTargetGUI component.</value>
    public MoveTargetGUI MoveTargetGUI
    {
        get {
            return _moveTgGUI;
        }
    }

    /// <summary>
    /// Component that handles squad units avatars GUI.
    /// </summary>
    /// <value>SquadUnitsGUI component.</value>
    public SquadUnitsGUI SquadUnitsGUI
    {
        get {
            return _sqUnitsGUI;
        }
    }

    /// <summary>
    /// Component that handles selection area GUI.
    /// </summary>
    /// <value>SelectSquareGUI component.</value>
    public SelectSquareGUI SelectSquareGUI {
        get {
            return _selectSqrGUI;
        }
    }

    /// <summary>
    /// Component that handles squad formation selection GUI.
    /// </summary>
    /// <value>SelectSquareGUI component.</value>
    public SquadFormationGUI SquadFormationGUI {
        get {
            return _sqFormationGUI;
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

    override protected void OnAwake()
    {
        _eventSystem = GetComponent<EventSystem>();
        _graphicRCasters = GetComponentsInChildren<GraphicRaycaster>();
    }
}