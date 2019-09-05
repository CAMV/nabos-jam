using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{

    [SerializeField]
    private Image _selectSqr;

    [SerializeField]
    private Image[] HealthBars;

    void Update()
    {
        for(int i = 0; i < GameManager.Instance.PlayerSquad.Units.Count; i++)
        {
            HealthBars[i].rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(GameManager.Instance.PlayerSquad.Units[i].transform.position);
        }
    }

    public void SetSelectSqr (Rect rect)
    {
        _selectSqr.rectTransform.anchoredPosition = rect.position;
        _selectSqr.rectTransform.sizeDelta = rect.size;
    }


}