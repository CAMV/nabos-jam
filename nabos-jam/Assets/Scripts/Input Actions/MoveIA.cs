using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Input Settings/Move Input Setting")]
public class MoveIA : InputAction
{
    private const float TIME_TO_SHOW_PREVIEW = .2f;

    [SerializeField]
    private int _terrainLayer = 8;
    
    public override IEnumerator ExecuteAction()
    {
        bool isPreviewOn = false;

        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
        int layerMask = 1 << _terrainLayer; 
        RaycastHit hit;
        List<MoveCmd> commandOut = new List<MoveCmd>();

        Debug.DrawRay(r.origin, r.direction*RAYCAST_LENGTH, Color.red, 5f);

        if (Physics.Raycast(r, out hit, RAYCAST_LENGTH, layerMask))
        {
            Unit leader = GameManager.Instance.PlayerSquad.ActiveUnits[0];
            
            Quaternion moveRot = leader.transform.rotation;
            moveRot.SetLookRotation(
                (hit.point - leader.transform.position).normalized,
                Vector3.up
            );

            Vector2 oldMPos;
            Vector2 newMPos = Input.mousePosition;
            float time = 0;

            List<Quaternion> rotations = new List<Quaternion>();
            List<Vector3> positions = new List<Vector3>();
            Matrix4x4 tPosMatrix = new Matrix4x4();
            
            Ray rPreview = Camera.main.ScreenPointToRay(newMPos);
            RaycastHit pHit = hit;

            Formation moveFormation = GameManager.Instance.PlayerSquad.Formation;

            do
            {   
                if (time >= TIME_TO_SHOW_PREVIEW)
                {

                    oldMPos = newMPos;
                    newMPos = Input.mousePosition;

                    if (oldMPos != newMPos)
                    {
                        rPreview = Camera.main.ScreenPointToRay(newMPos); 

                        if (Physics.Raycast(rPreview, out pHit, RAYCAST_LENGTH, layerMask))
                            moveRot = Quaternion.LookRotation(pHit.point - hit.point, Vector3.up);
                        
                    }

                    if (!isPreviewOn)
                    {

                        isPreviewOn = true;
                        GUIManager.Instance.MoveInFormationGUI.TurnOn();
                    }
                    
                }
                else
                {
                   time += Time.deltaTime; 
                }          

                if (isPreviewOn || rotations.Count == 0 || positions.Count == 0)
                {
                    rotations.Clear();
                    positions.Clear();

                    rotations.Add(moveRot);
                    positions.Add(hit.point);

                    tPosMatrix.SetTRS(hit.point, moveRot, Vector3.one);

                    for(int i = 0; i < GameManager.Instance.PlayerSquad.ActiveUnits.Count - 1; i++)
                    {
                        rotations.Add(Formation.GetFollowerRotation(
                                moveFormation, 
                                i,
                                tPosMatrix
                            ));

                        positions.Add(Formation.GetFollowerPosition(
                            moveFormation, 
                            i,
                            tPosMatrix
                        ));
                    }

                    GUIManager.Instance.MoveInFormationGUI.SetValues(positions, rotations);
                }
                
                yield return new WaitForEndOfFrame();

            } while (Input.GetButton(_buttomName));
            
            
                                                    
            for(int i = 0; i < GameManager.Instance.PlayerSquad.ActiveUnits.Count; i++)
            {
    
                Command fMoveCmd;

                // apply formation position offset
                if (moveFormation)
                {
                    fMoveCmd = new MoveCmd(
                            GameManager.Instance.PlayerSquad.ActiveUnits[i], 
                            positions[i],
                            rotations[i]
                        );
                }
                else
                {
                    fMoveCmd = new MoveCmd(
                                GameManager.Instance.PlayerSquad.ActiveUnits[i], 
                                positions[i]
                            );
                }
                
                GameManager.Instance.PlayerSquad.AddCommand(fMoveCmd);
            }

            GUIManager.Instance.MoveInFormationGUI.TurnOff();    
            GUIManager.Instance.MoveTargetGUI.Show(positions, rotations);        
        }
        
        yield return null;
    }
}