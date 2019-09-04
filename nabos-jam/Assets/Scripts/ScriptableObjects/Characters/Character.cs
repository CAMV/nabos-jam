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
    public int damage;
    public float attackSpeed;
    public float hitChance;
    public float dodgeChance;

    public void TakeDamage(int damage) {
        health.modifyHealth(-damage);
    }

    public bool HitCheck() 
    {
        float randVal = Random.Range(0, 1);
        return  randVal < hitChance;
    }

    public bool DodgeCheck()
    {
        float randVal = Random.Range(0, 1);
        return randVal > dodgeChance;
    }

    public void PerformAttack(Character enemyChar) {
        //Check character hit chance
        if (HitCheck()) 
        {
            //Check enemy dodgeChance
            if (enemyChar.DodgeCheck()) 
            {
                Attack(enemyChar);
            }
        }
    }

    public void Attack(Character enemyChar) 
    {
        enemyChar.TakeDamage(damage);
    }
}
