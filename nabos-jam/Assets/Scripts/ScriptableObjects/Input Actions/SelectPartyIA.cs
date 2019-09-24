using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using System.Collections; 

/// <summary>
/// Class <c> SelectPartyIa </c> models the action of selecting party member units.
/// </summary>
[CreateAssetMenu(menuName = "Input Settings/Select Party Input Setting")]
public class SelectPartyIA : InputAction
{
    /// <summary>
    /// Redifene ChekInput to not just evaluate the input, but also evaluate the mouse is not over any GUI element.
    /// </summary>
    /// <returns>True if the input is correct and the mouse s not over any GUI element, otherwise, false.</returns>
    override public bool CheckInput()
    {
        if (base.CheckInput())
        {
            if (!GUIManager.Instance.IsMouseOverGUI)
                return true;

        }
        return false;
    }

    /// <summary>
    /// Coroutine that creates and add a SelectCmd to the player's party controller based on the player's input.
    /// </summary>
    public override IEnumerator ExecuteAction()
    {
        // check if selected units are going to be reseted by new selected unit
        bool isReset = !Input.GetButton("Ctrl");

        List<Unit> selectedUnits = new List<Unit>();
        // Units that could be selected if not already
        List<Unit> nonSelectedUnits = new List<Unit>(GameManager.Instance.PlayerParty.Units);

        Vector2 startMousePos = Input.mousePosition;
        Vector2 currentMousePos = Input.mousePosition;

        Rect rect = new Rect();

        // Gets the rectangle area of selection
        while (Input.GetButton(_buttomName))
        {
            currentMousePos = Input.mousePosition;

            rect.x = startMousePos.x >= currentMousePos.x ? startMousePos.x : currentMousePos.x;
            rect.y = startMousePos.y >= currentMousePos.y ? startMousePos.y : currentMousePos.y;
            rect.width = Mathf.Abs(startMousePos.x - currentMousePos.x);
            rect.height = Mathf.Abs(startMousePos.y - currentMousePos.y);

            // GUI update
            if (GUIManager.Instance.ScreenSelectionGizmo)
                GUIManager.Instance.ScreenSelectionGizmo.Show(rect);

            yield return new WaitForEndOfFrame();
        }

        // Add any unit that was under the pointer adter releasing the input
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;

        for(int i = 0; i< nonSelectedUnits.Count; i++)
        {
            Unit u = nonSelectedUnits[i];
            if (u.SelectCollider && u.SelectCollider.Raycast(r, out hit, RAYCAST_LENGTH))
            {
                selectedUnits.Add(u);
                nonSelectedUnits.Remove(u);
                break;
            }
        }

        // Change rect position to represent the screenSpace correctly
        rect.x = startMousePos.x <= currentMousePos.x ? startMousePos.x : currentMousePos.x;
        rect.y = startMousePos.y <= currentMousePos.y ? startMousePos.y : currentMousePos.y;

        // Gets if any unit in screen space is within the selection area
        foreach (Unit u in nonSelectedUnits)
        {   
            Vector2 unitPos = Camera.main.WorldToScreenPoint(u.transform.position);
        
            if (rect.Contains(unitPos))
                selectedUnits.Add(u);
        }

        // if reset is not gonna happend, add newly selected units to existence active units
        if (!isReset)
        {
            foreach(Unit u in GameManager.Instance.PlayerParty.ActiveUnits)
            {
                if (!selectedUnits.Contains(u))
                {
                    selectedUnits.Add(u);
                }
            }
        }

        // GUI Update
        if (GUIManager.Instance.ScreenSelectionGizmo)
            GUIManager.Instance.ScreenSelectionGizmo.Hide();    

        GameManager.Instance.PlayerParty.AddCommand(new SelectCmd(selectedUnits));

    }
}