using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main character's class that has base attributes for the class,
/// as well as othe relevant information
/// </summary>
[CreateAssetMenu(menuName = "Character/CharacterClass")]
public class CharacterClass : ScriptableObject
{
    public string charClassName;
    public Stats baseStats;
}
