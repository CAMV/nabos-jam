using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Input Settings/Squad Unit GUI Input")]
public class SquadUnitsGUIIA : InputAction 
{
    private const float TIME_TO_DRAG = .2f; 

    public override IEnumerator ExecuteAction()
    {
        UnitAvatarGUI selectedAvatar = GetSelectedAvatar();

        if (selectedAvatar)
        {
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
                
                do
                {
                    if (elapsedTime >= TIME_TO_DRAG)
                    {
                        oldMPos = newMPos;
                        newMPos = Input.mousePosition;

                        if (oldMPos != newMPos)
                            GUIManager.Instance.SquadUnitsGUI.UpdateDrag(newMPos);

                        if (!isDragginMode)
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

                if (elapsedTime > TIME_TO_DRAG)
                    GUIManager.Instance.SquadUnitsGUI.StopDrag();                    
                else
                    SelectionAction(selectedAvatar);

            } 
        }
      
    }

    // If the mouse is over an avatar, return it. Else, return null. 
    private UnitAvatarGUI GetSelectedAvatar()
    {
        int nAvatars =  GameManager.Instance.PlayerSquad.Size;
        UnitAvatarGUI[] avatars = GUIManager.Instance.SquadUnitsGUI.Avatars;

        UnitAvatarGUI selectedAvatar = null;

        for (int i = 0; i < nAvatars; i++)
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

    // Execute the selection of unit action, selecting the unit corresponding to the avatar selected
    private void SelectionAction(UnitAvatarGUI sAvatar, bool isCtrlDown = false)
    {
        List<Unit> unitsSelected;

        if (isCtrlDown)
        {
            unitsSelected = new List<Unit>(GameManager.Instance.PlayerSquad.ActiveUnits);
            if (!unitsSelected.Contains(sAvatar.Unit))
                unitsSelected.Add(sAvatar.Unit);
            else
                unitsSelected.Clear();
        }
        else
        {
            unitsSelected = new List<Unit>() {sAvatar.Unit};
        }
    
        GameManager.Instance.PlayerSquad.AddCommand( new SelectCmd(unitsSelected));       
    }

}