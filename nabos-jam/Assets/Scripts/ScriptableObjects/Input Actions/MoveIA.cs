using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class <c> MoveIA </c> models the action of moving the active units by te player.
/// </summary>
[CreateAssetMenu(menuName = "Input Settings/Move Input Setting")]
public class MoveIA : InputAction
{
    private const float TIME_TO_SHOW_PREVIEW = .2f;

    [SerializeField]
    private int _terrainLayer = 8;
    
    /// <summary>
    /// Coroutine that creates and add a <c>MoveSquadCmd</c> to the player's squad manager based on the input given by the player
    /// </summary>
    public override IEnumerator ExecuteAction()
    {
        bool isPreviewOn = false;

        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
        int layerMask = 1 << _terrainLayer; 
        RaycastHit hit;
        List<MoveCmd> commandOut = new List<MoveCmd>();

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
            float elapsedTime = 0;

            List<Quaternion> rotations = new List<Quaternion>();
            List<Vector3> positions = new List<Vector3>();
            Matrix4x4 tPosMatrix = new Matrix4x4();
            
            Ray rPreview = Camera.main.ScreenPointToRay(newMPos);
            RaycastHit pHit = hit;

            Formation moveFormation = GameManager.Instance.PlayerSquad.Formation;

            do
            {   
                // Checks if preview must be shown
                if (elapsedTime >= TIME_TO_SHOW_PREVIEW)
                {

                    oldMPos = newMPos;
                    newMPos = Input.mousePosition;

                    // gets new traget of the rotation based on the preview
                    if (oldMPos != newMPos)
                    {
                        rPreview = Camera.main.ScreenPointToRay(newMPos); 

                        if (Physics.Raycast(rPreview, out pHit, RAYCAST_LENGTH, layerMask))
                            moveRot = Quaternion.LookRotation(pHit.point - hit.point, Vector3.up);
                        
                    }

                    if (!isPreviewOn)
                    {
                        isPreviewOn = true;

                        // Turn on the formation preview gizmos
                        if (GUIManager.Instance.MoveInFormationGUI)
                            GUIManager.Instance.MoveInFormationGUI.SetActive(true);
                    }
                    
                }
                else
                {
                   elapsedTime += Time.deltaTime; 
                }          


                // Calculate the target positions and rotations of the followers
                if (isPreviewOn || rotations.Count == 0 || positions.Count == 0)
                {
                    rotations.Clear();
                    positions.Clear();

                    // Add leaders target postion and rotations first
                    rotations.Add(moveRot);
                    positions.Add(hit.point);

                    tPosMatrix.SetTRS(hit.point, moveRot, Vector3.one);

                    // Add followers target position and rotation
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

                    // Updates the preview gizmo values
                    if (GUIManager.Instance.MoveInFormationGUI)
                        GUIManager.Instance.MoveInFormationGUI.SetValues(positions, rotations);
                }
                
                yield return new WaitForEndOfFrame();

            } while (Input.GetButton(_buttomName)); 

            Command fMoveCmd;

            // apply formation position offset
            if (moveFormation)
            {
                fMoveCmd = new MoveSquadCmd(
                                    GameManager.Instance.PlayerSquad.ActiveUnits,
                                    positions[0],
                                    rotations[0],
                                    moveFormation
                                );
            }
            else
            {
                fMoveCmd = new MoveSquadCmd(
                                    GameManager.Instance.PlayerSquad.ActiveUnits,
                                    positions[0],
                                    moveFormation
                                );
            }           

            GameManager.Instance.PlayerSquad.AddCommand(fMoveCmd);

            // Turn on preview gizmos and shows the movement feedback
            if (GUIManager.Instance.MoveInFormationGUI)
                GUIManager.Instance.MoveInFormationGUI.SetActive(false);    

            if (GUIManager.Instance.MoveTargetGUI)
                GUIManager.Instance.MoveTargetGUI.Show(positions, rotations);        
        }
        
        yield return null;
    }
}