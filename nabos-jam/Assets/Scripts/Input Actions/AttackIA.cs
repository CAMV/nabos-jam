using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles an attack input action pattern
/// </summary>
[CreateAssetMenu(menuName = "Input Settings/Attack Input Settings")]
public class AttackIA : InputAction
{
    [SerializeField]
    private int _enemyLayer;

    /// <summary>
    /// Adds an attack command to the player's units via the
    /// squad manager when an input is pressed.
    /// </summary>
    public override IEnumerator ExecuteAction()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); 
        int layerMask = 1 << _enemyLayer; 
        RaycastHit hit;

        Debug.DrawRay(r.origin, r.direction*RAYCAST_LENGTH, Color.red, 5f);

        if (Physics.Raycast(r, out hit, RAYCAST_LENGTH, layerMask))
        {
            //Get targetted unit
            Unit enemy = hit.transform.GetComponent<Unit>();
            for(int i = 0; i < GameManager.Instance.PlayerSquad.ActiveUnits.Count; i++) 
            {
                //Queue attack command from unit to target
                Unit unit = GameManager.Instance.PlayerSquad.ActiveUnits[i];
                GameManager.Instance.PlayerSquad.AddCommand(new AttackCmd(unit, enemy));
            }
        }
        
        yield return null;
    }
}
