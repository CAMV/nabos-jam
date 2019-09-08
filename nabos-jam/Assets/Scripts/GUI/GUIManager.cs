using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Singleton <c> GUI Manager </c> models all GUI components, giving access to all of them.
/// </summary>
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

    

}