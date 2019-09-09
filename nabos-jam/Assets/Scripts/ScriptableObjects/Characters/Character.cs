using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main <c>character</c> class that has all the information related to a character.
/// This class also handles attacking
/// </summary>
[CreateAssetMenu(menuName = "Character/Character")]
public class Character : ScriptableObject
{
    public string characterName;        //Name ID
    public Health health;               //Health Resource
    public Health shieldHealth;         //Shield Resource
    public float attackCooldown;
    public float attackRange;

    public Stats stats;
    public CharacterClass charClass;

    public FormationsEnum formationType;

    /// <summary>
    /// Performs damage reduction calculations and take the remaining damage
    /// </summary>
    /// <param name="damage">The amount of damage to that will be taken, as a positive value</param>
    public void TakeDamage(int damage) 
    {
        //Reduce damage by armor
        float armorAbsorption = Random.Range(0.3f, 1) * stats.armor.finalStat;
        int remainingDamage = (int) Mathf.Max(0, damage - armorAbsorption);
        //Reduce health from shield
        if (remainingDamage > 0 && 
            shieldHealth.currentHealth != 0) 
        {
            remainingDamage = damage - shieldHealth.currentHealth;
            shieldHealth.modifyHealth(-damage);
        }
        //Reduce actual health
        if (remainingDamage > 0) 
        {
            health.modifyHealth(-remainingDamage);
        }
        Debug.Log(characterName + " took damage. remaining health " + health.currentHealth);
    }

    /// <summary>
    /// Reduce attack delay and Modifier cooldowns
    /// </summary>
    /// <param name="tick">A timestep, such as <c>Time.fixedDeltaTime</param>
    public void TickDurations(float tick)
    {
        //Reduce attack cd here because it is not part of the stat values
        if (attackCooldown > 0)
        {
            attackCooldown -= tick;
        }
        stats.ReduceModifierDurations(tick);
    }

    /// <summary>
    /// Attempt an attack on an enemy unit
    /// </summary>
    /// <param name="enemyChar">The enemy unit to attack</param>
    public void PerformAttack(Character enemyChar) 
    {
        //Check character hit chance
        if (stats.HitCheck()) 
        {
            //Check enemy dodgeChance
            if (enemyChar.stats.hasDodged()) 
            {
                //Attack the enemy
                Attack(enemyChar);
            }
            else
            {
                Debug.Log(enemyChar.characterName + " dodged attack");
            }
        }
        else
        {
            Debug.Log(characterName + " missed attack");
        }
    }

    /// <summary>
    /// Actual attack once hit and dodge checks passed
    /// </summary>
    /// <param name="enemyChar">The enemy to perform the attack on</param>
    public void Attack(Character enemyChar) 
    {
        enemyChar.TakeDamage(stats.damage.finalStat);
    }

    /// <summary>
    /// Set attack delay equal to character attack speed
    /// </summary>
    public void ResetAttackCd() 
    {
        attackCooldown = stats.attackSpeed.finalStat;
    }
}
