using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Health")]
public class Health : Resource
{
    [SerializeField]
    public int maxHealth {
        get {
            return maxResource;
        }
        set {
            maxHealth = value;
        }
    }
    [SerializeField]
    public int currentHealth {
        get {
            return currentResource;
        }
        set {
            currentHealth = value;
        }
    }

    public Health(int maxHealth) 
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }

    public void modifyHealth(int healthMod)
    {
        modifyResource(healthMod);  
    }
}
