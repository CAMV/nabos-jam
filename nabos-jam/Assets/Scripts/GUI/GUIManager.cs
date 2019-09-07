using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{

    [SerializeField]
    private Image _selectSqr = null;

    [SerializeField]
    private MoveInFormationGUI _moveInFormGUI = null;

    [SerializeField]
    private MoveTargetGUI _moveTgGUI = null;

    [SerializeField]
    private SquadUnitsGUI _sqUnitsGUI = null;

    public MoveInFormationGUI MoveInFormationGUI
    {
        get {
            return _moveInFormGUI;
        }
    } 

    public MoveTargetGUI MoveTargetGUI
    {
        get {
            return _moveTgGUI;
        }
    }

    public SquadUnitsGUI SquadUnitsGUI
    {
        get {
            return _sqUnitsGUI;
        }
    }

    public void SetSelectSqr (Rect rect)
    {
        _selectSqr.rectTransform.anchoredPosition = rect.position;
        _selectSqr.rectTransform.sizeDelta = rect.size;
    }

}