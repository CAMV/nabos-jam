using UnityEngine;

/// <summary>
/// Class <c> Hotbar </c> handles the hotbar of the player to set hotkeys for actions.
/// </summary>
public class Hotbar : MonoBehaviour
{
    //  ( ͡° ͜ʖ ͡°)
    private const int N_OF_SLOTS = 8;

    [SerializeField]
    private Skill[] _slots = new Skill[N_OF_SLOTS];


    /// <summary>
    /// Indexer to handle the slots as an array
    /// </summary>
    /// <value></value>
    public Skill this[int index] {
        get {
            return _slots[index];
        }

        set {
            _slots[index] = value;
        }
    }

}