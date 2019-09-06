using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{

    [SerializeField]
    private Image _selectSqr;

    void Update()
    {
        
    }

    public void SetSelectSqr (Rect rect)
    {
        _selectSqr.rectTransform.anchoredPosition = rect.position;
        _selectSqr.rectTransform.sizeDelta = rect.size;
    }


}