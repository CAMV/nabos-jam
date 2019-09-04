using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
    public string nameToMod;    //Name of the stat that will be modifies
    public Modifier modifier;

    public void assignModifier(Stat stat) {

        stat.AddModifier(modifier);
    }
}
