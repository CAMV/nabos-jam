using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class <c> UnitAvatar </c> models a single unit avatar GUI. 
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class Avatar : MonoBehaviour, IPointerClickHandler
{
 
    [SerializeField]
    private AvatarSkillSlots _skillGUI = null;

    [SerializeField]
    private Unit _myUnit;

    [SerializeField]
    private Image _portrait = null;
    
    [SerializeField]
    private Text _showingName = null;

    [SerializeField]
    private GameObject _selectGizmo = null;

    private bool _isExpanded;
    private RectTransform _myRectTransform;
    private Canvas _myCanvas;

    /// <summary>
    /// Unit that the avatar is representing.
    /// </summary>
    /// <value>Unit.</value>
    public Unit Unit
    {
        get { 
            return _myUnit;
        }

        set {
            if (value == _myUnit)
            return;
        
            if (value.GUIData)
            {
                if (_portrait)
                    _portrait.sprite = value.GUIData.Icon;
                else
                    Debug.Log("Can't display portrait, no Image UI for the Avatar " + gameObject.name + "given!");                    

                if (_showingName)
                    _showingName.text = value.GUIData.Name;
                else
                    Debug.Log("Can't display name, no Text UI for the Avatar " + gameObject.name + " given!");

            }

            List<Unit> aUnits = GameManager.Instance.PlayerParty.ActiveUnits;

            if (aUnits != null) 
                SetSelectGizmo(aUnits.Contains(value));


            // Init skill component if it has one
            if (value.Skills)
            {
                for ( int i = 0; i < _skillGUI.Size; i++)
                {
                    if (i < value.Skills.Size)
                        _skillGUI[i] = value.Skills[i];
                    else
                        _skillGUI[i] = null;
                }

            }
        
            _myUnit = value;
        }
    }

    /// <summary>
    /// Skill GUI componenet of the Avatar
    /// </summary>
    public AvatarSkillSlots SkillGUI {
        get {
            return _skillGUI;
        }
    }

    void Start()
    {

        if (_myUnit && _myUnit.GUIData)
        {
            _portrait.sprite = _myUnit.GUIData.Icon;
            _showingName.text = _myUnit.GUIData.Name;
        }

        _myRectTransform = GetComponent<RectTransform>();
        _myCanvas = GetComponentInParent<Canvas>().rootCanvas;
    }

    /// <summary>
    /// Turns on/off the highlight of the avatar.
    /// </summary>
    /// <param name="isActive"> is avatar highlighted.</param>
    public void SetSelectGizmo(bool isActive)
    {
        if (_selectGizmo)
            _selectGizmo.SetActive(isActive);
    } 

    /// <summary>
    /// Action to execute when Avatar is clicked. The click comes from _picture.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick (PointerEventData eventData)
    {
        if (eventData.dragging)
            return;
        
        // If an skill slot was clicked, skip draggin
        if (eventData.rawPointerPress.GetComponentInParent<HotbarSlot>())
            return;
            
        SelectCmd selectCmd;
        List<Unit> selectedUnits;

        if (Input.GetButton("Ctrl"))
        {
            selectedUnits = new List<Unit>(GameManager.Instance.PlayerParty.ActiveUnits);

            if (selectedUnits.Contains(_myUnit))
                selectedUnits.Remove(_myUnit);
            else
                selectedUnits.Add(_myUnit);

        }
        else
        {
            selectedUnits = new List<Unit>();
            selectedUnits.Add(_myUnit);
         
        }
        selectCmd = new SelectCmd(selectedUnits);   

        //StartCoroutine(CheckIfDraggingCO(selectCmd));
        GameManager.Instance.PlayerParty.AddCommand(selectCmd);
    }

}