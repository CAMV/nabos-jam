using UnityEngine;
using System.Collections;

/// <summary>
/// Input Action that handles hotkeys for the hotbar slots as well as reordering them
/// </summary>
[CreateAssetMenu(menuName = "Input Settings/Hotbar Input Settings")]
public class HotbarIA : InputAction
{
    [SerializeField]
    private int _hotbarIndex = 0;

    /// <summary>
    /// Execute the hotbarslot Action according to the index
    /// </summary>
    public override IEnumerator ExecuteAction()
    {
        bool isQuickCast = Input.GetButton("Shift");

        if (!GUIManager.Instance.Hotbar)
            Debug.Log("No hotbar GUI component instanced!");
        else if (_hotbarIndex >= GUIManager.Instance.Hotbar.Size || _hotbarIndex < 0)
            Debug.Log("Invalid hotbar slot number!");
        else if (GUIManager.Instance.Hotbar[_hotbarIndex] == null)
            Debug.Log("Slot is empty!");
        else
            GUIManager.Instance.Hotbar[_hotbarIndex].Execute(isQuickCast || InputController.isAlwaysQuickCast);
        
        yield return null;
    }
    
}
