using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// GUI component that hadles the changes of formation for the player's squad using a dropdown menu component.
/// </summary>
public class SquadFormationGUI : MonoBehaviour
{
    [SerializeField]
    private Dropdown _dropdownMenu = null;

    /// <summary>
    /// Set the options of the dropdown menu based on the formations availables in the Game Manager.
    /// </summary>
    public void Initialize()
    {
        _dropdownMenu.ClearOptions();

        List<Sprite> options = new List<Sprite>();

        foreach(var formation in GameManager.Instance.FormationAvailable)
        {
            options.Add(formation.Icon);
        }

        _dropdownMenu.AddOptions(options);
    }

    void Start()
    {
        Initialize();
    }

    public void UpdateFormation()
    {
        if (_dropdownMenu)
             GameManager.Instance.ChangePlayerSquadFormation(_dropdownMenu.value); 
    }
}