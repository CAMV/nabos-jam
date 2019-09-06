using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Input Settings/Move Input Setting")]
public class MoveIA : InputAction
{
    [SerializeField]
    private int _terrainLayer;
    
    public override IEnumerator ExecuteAction()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
        int layerMask = 1 << _terrainLayer; 
        RaycastHit hit;

        Debug.DrawRay(r.origin, r.direction*RAYCAST_LENGTH, Color.red, 5f);

        if (Physics.Raycast(r, out hit, RAYCAST_LENGTH, layerMask))
        {
            Unit leader = GameManager.Instance.PlayerSquad.ActiveUnits[0];
            
            Quaternion moveRot = leader.transform.rotation;
            moveRot.SetLookRotation(
                (hit.point - leader.transform.position).normalized,
                Vector3.up
            );

            GameManager.Instance.PlayerSquad.AddCommand(new MoveCmd(leader, hit.point, moveRot));

            Formation moveFormation = GameManager.Instance.PlayerSquad.Formation;
                                                    
            for(int i = 1; i < GameManager.Instance.PlayerSquad.ActiveUnits.Count; i++)
            {
                Matrix4x4 tPosMatrix = new Matrix4x4();
                tPosMatrix.SetTRS(hit.point, moveRot, Vector3.one);

                Vector3 currentHitPoint = hit.point;
                Command fMoveCmd;

                // apply formation position offset
                if (moveFormation)
                {
                    currentHitPoint = tPosMatrix.MultiplyPoint(
                                            moveFormation.GetPosOffset(i-1)
                                        );

                    Quaternion cRot =  tPosMatrix.rotation * Quaternion.Euler(0, moveFormation.GetEAOffset(i), 0);

                    fMoveCmd = new MoveCmd(
                            GameManager.Instance.PlayerSquad.ActiveUnits[i], 
                            currentHitPoint,
                            cRot
                        );
                }
                else
                {
                    fMoveCmd = new MoveCmd(
                                GameManager.Instance.PlayerSquad.ActiveUnits[i], 
                                currentHitPoint
                            );
                }

                GameManager.Instance.PlayerSquad.AddCommand(fMoveCmd);
            }
        }
        
        yield return null;
    }
}