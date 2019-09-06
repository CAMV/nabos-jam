﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/CharacterClass")]
public class CharacterClass : ScriptableObject
{
    public string charClassName;
    public Stats baseStats;
}
