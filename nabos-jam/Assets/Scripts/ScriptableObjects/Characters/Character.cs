using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public Health health;
    public Health shieldHealth;

    public Stats stats;
    public CharacterClass charClass;

    public FormationsEnum formationType;

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
    }


    public void PerformAttack(Character enemyChar) 
    {
        //Check character hit chance
        if (stats.HitCheck()) 
        {
            //Check enemy dodgeChance
            if (!enemyChar.stats.hasDodged()) 
            {
                Attack(enemyChar);
            }
        }
    }

    public void Attack(Character enemyChar) 
    {
        enemyChar.TakeDamage(stats.damage.finalStat);
    }
}
