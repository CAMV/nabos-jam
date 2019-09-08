using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class SquadUnitsGUI : MonoBehaviour, IDragComponent<UnitAvatarGUI>
{
    [SerializeField]
    private GameObject _dragDummy = null;  

    [SerializeField]
    private UnitAvatarGUI[] _uAvatars = new UnitAvatarGUI[0];

    private int _indexDragElement = 0;
    private int _nActiveAvatars = 0;

    public UnitAvatarGUI[] Avatars
    {
        get {
            return _uAvatars;
        }
    }

    void Start()
    {
        List<Unit> units = GameManager.Instance.PlayerSquad.Units;
        _dragDummy.SetActive(false);
        
        for (int i = 0; i < units.Count; i++)
        {
            _uAvatars[i].gameObject.SetActive(true);
            _uAvatars[i].Unit = units[i];
        } 

        _nActiveAvatars = units.Count;           
    }

    // Higlight the avatar of the given units
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

    // IDragComponent functions
    public void StartDrag(UnitAvatarGUI elementToDrag)
    {
        elementToDrag.gameObject.SetActive(false);
        _dragDummy.transform.position = elementToDrag.gameObject.transform.position;
        _dragDummy.GetComponent<UnitAvatarGUI>().Unit = elementToDrag.Unit;
        _indexDragElement = Array.IndexOf(_uAvatars, elementToDrag);
        _dragDummy.SetActive(true);
    }

    public void UpdateDrag(Vector2 mousePos)
    {
        _dragDummy.transform.position = new Vector3(
                                                _dragDummy.transform.position.x, 
                                                mousePos.y, 
                                                _dragDummy.transform.position.z
                                            );

        int firstIndex = _indexDragElement;
        int secondIndex = firstIndex;

        if (firstIndex < _nActiveAvatars-1 && _dragDummy.transform.position.y < _uAvatars[firstIndex+1].transform.position.y )
        {
            secondIndex = firstIndex+1;
        }
        else if (firstIndex > 0 && _dragDummy.transform.position.y > _uAvatars[firstIndex-1].transform.position.y )
        {
           secondIndex = firstIndex-1; 
        }

        if (firstIndex != secondIndex)
        {
            var auxUnit = _uAvatars[firstIndex].Unit;
            
            _uAvatars[firstIndex].Unit = _uAvatars[secondIndex].Unit;
            _uAvatars[secondIndex].Unit = auxUnit;

            _uAvatars[firstIndex].gameObject.SetActive(true);
            _uAvatars[secondIndex].gameObject.SetActive(false);

            GameManager.Instance.PlayerSquad.SwapUnitsOrer(firstIndex, secondIndex);

            _indexDragElement = secondIndex;
        }
        
    }

    public void StopDrag()
    {
        _dragDummy.SetActive(false);
        _uAvatars[_indexDragElement].gameObject.SetActive(true);
    }
}