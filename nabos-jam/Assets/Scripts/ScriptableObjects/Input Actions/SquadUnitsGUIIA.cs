using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class <c> SquadUnitsGUIIA </c> models the action of selecting squad member units in the avatar GUI as well as reordering the squad members.
/// </summary>
[CreateAssetMenu(menuName = "Input Settings/Squad Unit GUI Input")]
public class SquadUnitsGUIIA : InputAction 
{
    private const float TIME_TO_DRAG = .2f; 

    /// <summary>
    /// Coroutine that creates a selectCmd or reorder the player's squad units based on the player's input.
    /// </summary>
    public override IEnumerator ExecuteAction()
    {
        // Check if there is any avatar clicked
        UnitAvatarGUI selectedAvatar = GetSelectedAvatar();

        if (selectedAvatar)
        {
            // Gets input modifier
           if (Input.GetButton("Ctrl"))
            {
                SelectionAction(selectedAvatar, true);
            }
            else
            {
                float elapsedTime = 0;

                Vector2 oldMPos;
                Vector2 newMPos = Input.mousePosition;

                bool isDragginMode = false;
                
                // Checks if second behaviour, reordering squad units, is the desired behaviour
                do
                {
                    // If reorderins is wanted, start the dragging behaviour
                    if (elapsedTime >= TIME_TO_DRAG)
                    {
                        oldMPos = newMPos;
                        newMPos = Input.mousePosition;

                        if (GUIManager.Instance.SquadUnitsGUI && oldMPos != newMPos)
                            GUIManager.Instance.SquadUnitsGUI.UpdateDrag(newMPos);

                        if (GUIManager.Instance.SquadUnitsGUI && !isDragginMode)
                        {
                            GUIManager.Instance.SquadUnitsGUI.StartDrag(selectedAvatar);
                            isDragginMode = true;
                        }                       
                    }
                    else
                    {
                        elapsedTime += Time.deltaTime; 
                    } 

                    yield return new WaitForEndOfFrame();

                } while (Input.GetButton(_buttomName));

                if (isDragginMode)
                    GUIManager.Instance.SquadUnitsGUI.StopDrag();                    
                else
                    SelectionAction(selectedAvatar);

            } 
        }
      
    }

    /// <summary>
    /// If the mouse is over an avatar, return it. Else, return null. 
    /// </summary>
    /// <returns>UnitAvatarGUI where the mouse pointer is over</returns>
    private UnitAvatarGUI GetSelectedAvatar()
    {
        UnitAvatarGUI[] avatars = new UnitAvatarGUI[0]; 
        
        if (GUIManager.Instance.SquadUnitsGUI)
            avatars = GUIManager.Instance.SquadUnitsGUI.Avatars;

        UnitAvatarGUI selectedAvatar = null;

        for (int i = 0; i < avatars.Length; i++)
        {
            Rect aRect = avatars[i].GetScreenSpaceRect();
            
            if (aRect.Contains(Input.mousePosition))
            {
                selectedAvatar = avatars[i];
                return selectedAvatar;
            }
        }

        return null;
    }

    /// <summary>
    /// Creates a selectCmd for the selected avatar's unit, adding the unit, substracting the unit or resetting the active units.
    /// </summary>
    /// <param name="sAvatar">UnitAvatarGUI selected by the player</param>
    /// <param name="isCtrlDown">If control is active, perform an additive or substracting operation to the active units</param>
    private void SelectionAction(UnitAvatarGUI sAvatar, bool isCtrlDown = false)
    {
        List<Unit> unitsSelected;

        if (isCtrlDown)
        {
            unitsSelected = new List<Unit>(GameManager.Instance.PlayerSquad.ActiveUnits);
            if (!unitsSelected.Contains(sAvatar.Unit))
                unitsSelected.Add(sAvatar.Unit);
            else
                unitsSelected.Remove(sAvatar.Unit);
        }
        else
        {
            unitsSelected = new List<Unit>() {sAvatar.Unit};
        }
    
        GameManager.Instance.PlayerSquad.AddCommand( new SelectCmd(unitsSelected));       
    }

}