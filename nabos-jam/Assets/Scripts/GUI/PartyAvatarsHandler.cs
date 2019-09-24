using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

/// <summary>
/// Class <c> PartyUnitGUI </c> handles all avatars in the GUI of the units in the party. Setting them and reordering them with a drag & drop behaviour
/// </summary>
public class PartyAvatarsHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject _dragDummy = null;  
    [SerializeField]
    private Avatar[] _uAvatars = new Avatar[0];

    private int _indexDragElement = 0;
    private int _nActiveAvatars = 0;
    private bool _isDragging = false;

    void Start()
    {
        _dragDummy.SetActive(false);
        _isDragging = false;
        
        List<Unit> units = GameManager.Instance.PlayerParty.Units;
        for (int i = 0; i < units.Count; i++)
        {
            _uAvatars[i].gameObject.SetActive(true);
            _uAvatars[i].Unit = units[i];
        } 

        _nActiveAvatars = units.Count;           
    }

    /// <summary>
    /// Highlights the avatars of the given units to show which units are the active ones in the party.
    /// </summary>
    /// <param name="units">Units to be highlighted</param>
    public void SetSelectedAvatars(List<Unit> units)
    {
        foreach(var avatar in _uAvatars)
        {
            if (units.Contains(avatar.Unit))
                avatar.SetSelectGizmo(true);
            else
                avatar.SetSelectGizmo(false);
        }
    }

    // IDragComponent functions to handle re ordering of the party input

    /// <summary>
    /// Starts the dragging of a given avatar.
    /// </summary>
    public void OnBeginDrag(PointerEventData e)
    {
        if (_isDragging)
            return;

        Avatar avatarToDrag = e.pointerPressRaycast.gameObject.GetComponentInParent<Avatar>();
        _isDragging = true;

        if (avatarToDrag)
        {
            avatarToDrag.gameObject.SetActive(false);
            _dragDummy.transform.position = avatarToDrag.gameObject.transform.position;
            _dragDummy.GetComponent<Avatar>().Unit = avatarToDrag.Unit;
            _indexDragElement = Array.IndexOf(_uAvatars, avatarToDrag);
            _dragDummy.SetActive(true);    
        }
        
    }

    /// <summary>
    /// Updates the position of the avatar being dragged and change the order of the party units if tha avatar is dragged to the y-position of another avatar
    /// </summary>
    public void OnDrag(PointerEventData e)  
    {
        if (!_isDragging)
            return;

        // update avatar being dragged position
        _dragDummy.transform.position = new Vector3(
                                                _dragDummy.transform.position.x, 
                                                e.position.y, 
                                                _dragDummy.transform.position.z
                                            );

        int firstIndex = _indexDragElement;
        int secondIndex = firstIndex;

        // checks if the dragged avatar is in the position of another one
        if (firstIndex < _nActiveAvatars-1 && _dragDummy.transform.position.y < _uAvatars[firstIndex+1].transform.position.y )
        {
            secondIndex = firstIndex+1;
        }
        else if (firstIndex > 0 && _dragDummy.transform.position.y > _uAvatars[firstIndex-1].transform.position.y )
        {
           secondIndex = firstIndex-1; 
        }

        // Interchange avatars
        if (firstIndex != secondIndex)
        {
            var auxUnit = _uAvatars[firstIndex].Unit;
            
            _uAvatars[firstIndex].Unit = _uAvatars[secondIndex].Unit;
            _uAvatars[secondIndex].Unit = auxUnit;

            _uAvatars[firstIndex].gameObject.SetActive(true);
            _uAvatars[secondIndex].gameObject.SetActive(false);

            GameManager.Instance.PlayerParty.SwapUnitsOrer(firstIndex, secondIndex);

            _indexDragElement = secondIndex;
        }
        
    }

    /// <summary>
    /// Stops the drag of the avatar being dragged.
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isDragging)
            return;

        _isDragging = false;
        _dragDummy.SetActive(false);
        _uAvatars[_indexDragElement].gameObject.SetActive(true);
    }

}