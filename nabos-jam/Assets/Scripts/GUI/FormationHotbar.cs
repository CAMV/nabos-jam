using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// GUI component that hadles the changes of formation for the player's party using a dropdown menu component.
/// </summary>
public class FormationHotbar : MonoBehaviour
{
    [SerializeField]
    private Dropdown _dropdownMenu = null;

    [SerializeField]
    private HotbarSlot _currentFormation;



    /// <summary>
    /// Set the options of the dropdown menu based on the formations availables in the Game Manager.
    /// </summary>
    public void Initialize()
    {
        _dropdownMenu.ClearOptions();

        List<Sprite> options = new List<Sprite>();

        foreach(var formation in GameManager.Instance.FormationAvailable)
        {
            options.Add(formation.GUIData.Icon);
        }

        _dropdownMenu.AddOptions(options);
    }

    void Start()
    {
        Initialize();
    }


}