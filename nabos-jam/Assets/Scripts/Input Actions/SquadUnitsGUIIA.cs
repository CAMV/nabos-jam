using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Input Settings/Squad Unit GUI Input")]
public class SquadUnitsGUIIA : InputAction
{
    public override IEnumerator ExecuteAction()
    {
        int nAvatars =  GameManager.Instance.PlayerSquad.Size;
        UnitAvatarGUI[] avatars = GUIManager.Instance.SquadUnitsGUI.Avatars;

        for (int i = 0; i < nAvatars; i++)
        {
            Rect aRect = avatars[i].GetScreenSpaceRect();
            
            if (aRect.Contains(Input.mousePosition))
            {
                List<Unit> unitsSelected;

                if (Input.GetButton("Ctrl"))
                {
                    unitsSelected = new List<Unit>(GameManager.Instance.PlayerSquad.ActiveUnits);
                    if (!unitsSelected.Contains(avatars[i].Unit))
                        unitsSelected.Add(avatars[i].Unit);
                    else
                        unitsSelected.Clear();
                }
                else
                {
                    unitsSelected = new List<Unit>() {avatars[i].Unit};
                }
            
                GameManager.Instance.PlayerSquad.AddCommand( new SelectCmd(unitsSelected));
                break;
            }
        }

        yield return null;
    }

}