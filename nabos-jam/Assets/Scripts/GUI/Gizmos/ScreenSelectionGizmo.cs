using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c> SelectSquareGUI </c> handles the square that show the area of unit selection when selecting units.
/// </summary>
public class ScreenSelectionGizmo : MonoBehaviour
{

    [SerializeField]
    private Image _selectSqr = null;

    /// <summary>
    /// Sets the area of the square that shows the area of selection.
    /// </summary>
    /// <param name="rect">Area of selection in canvas space coord.</param>
    public void Show (Rect rect)
    {
        _selectSqr.rectTransform.anchoredPosition = rect.position;
        _selectSqr.rectTransform.sizeDelta = rect.size;
    }

    public void Hide()
    {
        _selectSqr.rectTransform.anchoredPosition = Vector2.zero;
        _selectSqr.rectTransform.sizeDelta = Vector2.zero;
    }
}