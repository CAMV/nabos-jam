using UnityEngine;
using System.Collections.Generic;
using System.Collections; 

[CreateAssetMenu(menuName = "Input Settings/Select Squad Input Setting")]
public class SelectSquadIA : InputAction
{

    public override IEnumerator ExecuteAction()
    {
        List<Unit> sUnits = new List<Unit>();
        sUnits.Clear();

        if (Input.GetButton("Ctrl"))                    // Multiple unit selection with Ctrl pressed
        {
            Vector2 startMousePos = Input.mousePosition;
            Vector2 currentMousePos = Input.mousePosition;

            Rect r = new Rect();

            while (Input.GetButton(_buttomName))
            {
                currentMousePos = Input.mousePosition;

                r.x = startMousePos.x >= currentMousePos.x ? startMousePos.x : currentMousePos.x;
                r.y = startMousePos.y >= currentMousePos.y ? startMousePos.y : currentMousePos.y;
                r.width = Mathf.Abs(startMousePos.x - currentMousePos.x);
                r.height = Mathf.Abs(startMousePos.y - currentMousePos.y);

                GUIManager.Instance.SetSelectSqr(r);
                yield return new WaitForEndOfFrame();
            }

            r.x = startMousePos.x <= currentMousePos.x ? startMousePos.x : currentMousePos.x;
            r.y = startMousePos.y <= currentMousePos.y ? startMousePos.y : currentMousePos.y;

            foreach (Unit u in GameManager.Instance.PlayerSquad.Units)
            {   
                Vector2 unitPos = Camera.main.WorldToScreenPoint(u.transform.position);
                
                if (r.Contains(unitPos))
                    sUnits.Add(u);
            }

            GUIManager.Instance.SetSelectSqr(Rect.zero);
        }
        else                                            // single unit selection                                   
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            foreach(Unit u in GameManager.Instance.PlayerSquad.Units)
            {
                if (u.SelectCollider && u.SelectCollider.Raycast(r, out hit, RAYCAST_LENGTH))
                {
                    sUnits.Add(u);
                    break;   
                }
            }
        }

        GameManager.Instance.PlayerSquad.AddCommand(new SelectCmd(sUnits));

        yield return null;
    }
}