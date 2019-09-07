using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SquadUnitsGUI : MonoBehaviour
{
    [SerializeField]
    private UnitAvatarGUI[] _uAvatars = new UnitAvatarGUI[0];

    [SerializeField]
    private UnitAvatarGUI _dragDummy = null;

    public UnitAvatarGUI[] Avatars
    {
        get {
            return _uAvatars;
        }
    }

    void Start()
    {

        List<Unit> units = GameManager.Instance.PlayerSquad.Units;
        
        for (int i = 0; i < units.Count; i++)
        {
            _uAvatars[i].gameObject.SetActive(true);
            _uAvatars[i].Unit = units[i];
        }            
    }

    public void InterchangeAvatarUnits(int first, int second)
    {
        var auxUnit = _uAvatars[first].Unit;
        
        _uAvatars[first].Unit = _uAvatars[second].Unit;
        _uAvatars[second].Unit = auxUnit;
    }

    public RectTransform DragDummy {
        get {
            return _dragDummy.GetComponent<RectTransform>();
        }
    }

    public void SetActiveAvatars(List<Unit> units)
    {
        foreach(var avatar in _uAvatars)
        {
            if (units.Contains(avatar.Unit))
                avatar.SetSelectGizmo(true);
            else
                avatar.SetSelectGizmo(false);
        }
    }

}