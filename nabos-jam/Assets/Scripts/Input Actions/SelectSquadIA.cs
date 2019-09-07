using UnityEngine;
using System.Collections.Generic;
using System.Collections; 

[CreateAssetMenu(menuName = "Input Settings/Select Squad Input Setting")]
public class SelectSquadIA : InputAction
{

    public override IEnumerator ExecuteAction()
    {
        // check if selected units are going to be reset by new selected unit
        bool isReset = !Input.GetButton("Ctrl");

        List<Unit> selectedUnits = new List<Unit>();
        // Units that could be selected if not already
        List<Unit> nonSelectedUnits = new List<Unit>(GameManager.Instance.PlayerSquad.Units);

        selectedUnits.Clear();

        Vector2 startMousePos = Input.mousePosition;
        Vector2 currentMousePos = Input.mousePosition;

        Rect rect = new Rect();

        while (Input.GetButton(_buttomName))
        {
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

            currentMousePos = Input.mousePosition;

            rect.x = startMousePos.x >= currentMousePos.x ? startMousePos.x : currentMousePos.x;
            rect.y = startMousePos.y >= currentMousePos.y ? startMousePos.y : currentMousePos.y;
            rect.width = Mathf.Abs(startMousePos.x - currentMousePos.x);
            rect.height = Mathf.Abs(startMousePos.y - currentMousePos.y);

            GUIManager.Instance.SetSelectSqr(rect);
            yield return new WaitForEndOfFrame();
        }

        // change rect position to represent the screenSpace correctly
        rect.x = startMousePos.x <= currentMousePos.x ? startMousePos.x : currentMousePos.x;
        rect.y = startMousePos.y <= currentMousePos.y ? startMousePos.y : currentMousePos.y;

        foreach (Unit u in nonSelectedUnits)
        {   
            Vector2 unitPos = Camera.main.WorldToScreenPoint(u.transform.position);
        
            if (rect.Contains(unitPos))
                selectedUnits.Add(u);
        }

        // if reset is not gonna happend, add newly selected units to existence active units
        if (!isReset)
        {
            foreach(Unit u in GameManager.Instance.PlayerSquad.ActiveUnits)
            {
                if (!selectedUnits.Contains(u))
                {
                    selectedUnits.Add(u);
                }
            }
        }

        GUIManager.Instance.SetSelectSqr(Rect.zero);    
        GameManager.Instance.PlayerSquad.AddCommand(new SelectCmd(selectedUnits));

    }
}