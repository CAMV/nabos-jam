using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public class UnitAvatarGUI : MonoBehaviour
{
 
    [SerializeField]
    private Unit _myUnit;

    [SerializeField]
    private Image _picture = null;
    
    [SerializeField]
    private Text _showingName = null;

    [SerializeField]
    private GameObject _selectGizmo = null;

    private bool _isExpanded;
    private RectTransform _myRectTransform;
    private Canvas _myCanvas;

    public Unit Unit
    {
        get { 
            return _myUnit;
        }

        set {
            if (value == _myUnit)
            return;
        
            if (value.Bio)
            {
                _picture.sprite = value.Bio.Portrait;
                _showingName.text = value.Bio.Name;
            }

            List<Unit> aUnits = GameManager.Instance.PlayerSquad.ActiveUnits;

            if (aUnits != null) 
                SetSelectGizmo(aUnits.Contains(value));
        
            _myUnit = value;
        }
    }

    void Start()
    {
        if (_myUnit && _myUnit.Bio)
        {
            _picture.sprite = _myUnit.Bio.Portrait;
            _showingName.text = _myUnit.Bio.Name;
        }

        _myRectTransform = GetComponent<RectTransform>();
        _myCanvas = GetComponentInParent<Canvas>().rootCanvas;
    }

    public void SetSelectGizmo(bool isActive)
    {
        if (_selectGizmo)
            _selectGizmo.SetActive(isActive);
    } 

    public Rect GetScreenSpaceRect()
    {
        Rect rect = _myRectTransform.rect;
        rect.x +=_myRectTransform.position.x;
        rect.y +=_myRectTransform.position.y;

        return rect;
    }

}