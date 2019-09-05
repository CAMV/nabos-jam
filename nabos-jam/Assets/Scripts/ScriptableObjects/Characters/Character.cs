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
        int remainingDamage = damage;
        if (shieldHealth.currentHealth != 0) 
        {
            remainingDamage = damage - shieldHealth.currentHealth;
            shieldHealth.modifyHealth(-damage);
        }
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
            if (enemyChar.stats.DodgeCheck()) 
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
