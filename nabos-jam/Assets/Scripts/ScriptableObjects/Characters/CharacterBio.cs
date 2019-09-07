using UnityEngine;

[CreateAssetMenu(menuName = "Character/Bio")]
public class CharacterBio : ScriptableObject 
{

    [SerializeField]
    private string _name = "";

    [SerializeField]
    private Sprite _portraitImage = null;

    public string Name {
        get {
            return _name;
        }
    }

    public Sprite Portrait {
        get {
            return _portraitImage;
        }
    } 

}