using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{

    [SerializeField]
    private Image _selectSqr;

    [SerializeField]
    private MovementPreview _mvmntPreview;

    public MovementPreview MovementPreview
    {
        get {
            return _mvmntPreview;
        }
    } 

    void Update()
    {
        
    }

    public void SetSelectSqr (Rect rect)
    {
        _selectSqr.rectTransform.anchoredPosition = rect.position;
        _selectSqr.rectTransform.sizeDelta = rect.size;
    }

}