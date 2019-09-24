using UnityEngine;

public class GUIData : ScriptableObject
{
    [SerializeField]
    protected Sprite _icon = null;
    
    [SerializeField]
    protected string _name = "";

    [SerializeField]
    protected string _description = "";

    /// <summary>
    /// Name of the Property
    /// </summary>
    public string Name
    {
        get 
        {
            return _name;
        }
    }

    /// <summary>
    /// Icon of the Property
    /// </summary>
    public Sprite Icon {
        get {
            return _icon;
        }
    } 

    /// <summary>
    /// Description of the Property
    /// </summary>
    public string Description {
        get {
            return _description;
        }
    }
}