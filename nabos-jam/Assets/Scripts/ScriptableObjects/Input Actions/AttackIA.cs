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
    private int _enemyLayer = 0;

    /// <summary>
    /// Adds an attack command to the player's units via the
    /// party manager when an input is pressed.
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
            for(int i = 0; i < GameManager.Instance.PlayerParty.ActiveUnits.Count; i++) 
            {
                //Queue attack command from unit to target
                Unit unit = GameManager.Instance.PlayerParty.ActiveUnits[i];
                AttackHandler attackHandler = unit.GetComponent<AttackHandler>();
                if (attackHandler && !attackHandler.isAttacking)
                {
                    GameManager.Instance.PlayerParty.AddCommand(new AttackCmd(unit, enemy));
                }
            }

            yield return new WaitForEndOfFrame();
        }
        
        yield return null;
    }
}
