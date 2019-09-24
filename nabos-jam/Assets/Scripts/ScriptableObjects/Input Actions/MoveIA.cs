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
    
    /// <summary>
    /// Coroutine that creates and add a <c>MovePartyCmd</c> to the player's party manager based on the input given by the player
    /// </summary>
    public override IEnumerator ExecuteAction()
    {
        bool isPreviewOn = false;

        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
        int layerMask = 1 << TERRAIN_LAYER; 
        RaycastHit hit;
        List<MoveCmd> commandOut = new List<MoveCmd>();

        if (Physics.Raycast(r, out hit, RAYCAST_LENGTH, layerMask) && 
            Vector3.Angle(Vector3.up, hit.normal) < 60 &&           //Check the angle of the surface is not too steep
            hit.transform.tag == "Floor")                           //Check the object hitted is targged as floor
        {
            Unit leader = GameManager.Instance.PlayerParty.ActiveUnits[0];

            //Stops units from attacking
            // for (int i = 0; i < GameManager.Instance.PlayerParty.ActiveUnits.Count; i++) 
            // {
            //     Unit unit =  GameManager.Instance.PlayerParty.ActiveUnits[i];
            //     if (unit.GetComponent<AttackHandler>())
            //     {
            //         unit.GetComponent<AttackHandler>().StopAttacking();
            //     }
            // }
            
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

            Formation moveFormation = GameManager.Instance.PlayerParty.Formation;
            Plane previewPlane = new Plane(Vector3.up, hit.point);

            do
            {   
                // Checks if preview must be shown
                if (elapsedTime >= TIME_TO_SHOW_PREVIEW)
                {
                    float enter;

                    oldMPos = newMPos;
                    newMPos = Input.mousePosition;

                    // gets new traget of the rotation based on the preview
                    if (oldMPos != newMPos)
                    {
                        rPreview = Camera.main.ScreenPointToRay(newMPos); 
                        if (previewPlane.Raycast(rPreview, out enter))
                            moveRot = Quaternion.LookRotation(rPreview.GetPoint(enter) - hit.point, Vector3.up);
                        
                    }

                    if (!isPreviewOn)
                        isPreviewOn = true;
                    
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
                    for(int i = 0; i < GameManager.Instance.PlayerParty.ActiveUnits.Count - 1; i++)
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
                    if (GUIManager.Instance.FormationPreviewGizmo)
                        GUIManager.Instance.FormationPreviewGizmo.Show(positions, rotations);
                }
                
                yield return new WaitForEndOfFrame();

            } while (Input.GetButton(_buttomName)); 

            Command fMoveCmd;

            // apply formation position offset
            if (moveFormation)
            {
                fMoveCmd = new MovePartyCmd(
                                    GameManager.Instance.PlayerParty.ActiveUnits,
                                    positions[0],
                                    rotations[0],
                                    moveFormation
                                );
            }
            else
            {
                fMoveCmd = new MovePartyCmd(
                                    GameManager.Instance.PlayerParty.ActiveUnits,
                                    positions[0],
                                    moveFormation
                                );
            }           

            GameManager.Instance.PlayerParty.AddCommand(fMoveCmd);

            // Turn on preview gizmos and shows the movement feedback
            if (GUIManager.Instance.FormationPreviewGizmo)
                GUIManager.Instance.FormationPreviewGizmo.Hide();    

            if (GUIManager.Instance.MovementFeedbackGizmo)
                GUIManager.Instance.MovementFeedbackGizmo.Show(positions, rotations);        
        }
        
        yield return null;
    }
}