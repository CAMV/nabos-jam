using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c> SelectSquareGUI </c> handles the square that show the area of unit selection when selecting units.
/// </summary>
public class SelectSquareGUI : MonoBehaviour
{

    [SerializeField]
    private Image _selectSqr = null;

    /// <summary>
    /// Sets the area of the square that shows the area of selection.
    /// </summary>
    /// <param name="rect">Area of selection in canvas space coord.</param>
    public void SetSquare (Rect rect)
    {
        _selectSqr.rectTransform.anchoredPosition = rect.position;
        _selectSqr.rectTransform.sizeDelta = rect.size;
    }
}