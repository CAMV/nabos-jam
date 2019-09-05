using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Health")]
public class Health : ScriptableObject
{
    public int maxHealth {get; private set;}
    public int currentHealth {get; private set;}

    public Health(int maxHealth) 
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }

    public void modifyHealth(int healthMod) 
    {
        currentHealth += healthMod;
        if (currentHealth < 0) 
        {
            currentHealth = 0;
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
