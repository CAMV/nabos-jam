using UnityEngine;

/// <summary>
/// Data structure that holds biographic information of a character
/// </summary>
[CreateAssetMenu(menuName = "Character/Bio")]
public class CharacterBio : ScriptableObject 
{
    [SerializeField]
    private string _name = "";

    [SerializeField]
    private Sprite _portraitImage = null;

    /// <summary>
    /// Gets the name of the character.
    /// </summary>
    /// <value>Name of the character.</value>
    public string Name {
        get {
            return _name;
        }
    }

    /// <summary>
    /// Gets a sprite of the character.
    /// </summary>
    /// <value>Sprite of the portrait of the character.</value>
    public Sprite Portrait {
        get {
            return _portraitImage;
        }
    } 

}