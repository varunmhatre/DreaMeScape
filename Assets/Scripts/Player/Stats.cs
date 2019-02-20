using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] int presence;
    [SerializeField] public int unchargedDamage;
    public bool statsVisible;
    public bool hasAttacked;
    private bool charging;

    public int meterUnitsFilled;
    [SerializeField] public int maxMeter;

    public bool isEncumbered;
    public bool isEnemy;


    public void GainMeter(int amt)
    {
        
        meterUnitsFilled += amt;
        if (meterUnitsFilled > maxMeter)
        {
            meterUnitsFilled = maxMeter;
        }
    }

    public void EmptyMeter()
    {
        meterUnitsFilled = 0;
    }

    public void Die()
    {
        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            if (gameObject == CharacterManager.allAlliedCharacters[i])
            {
                CharacterManager.allAlliedCharacters.Remove(gameObject);
            }
        }

        for (int i = 0; i < CharacterManager.allCharacters.Count; i++)
        {
            if (gameObject == CharacterManager.allCharacters[i])
            {
                CharacterManager.allCharacters.Remove(gameObject);
            }
        }

        for (int i = 0; i < CharacterManager.allEnemyCharacters.Count; i++)
        {
            if (gameObject == CharacterManager.allEnemyCharacters[i])
            {
                CharacterManager.allEnemyCharacters.Remove(gameObject);
            }
        }

        Destroy(gameObject);
    }

    public void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int amt)
    {
        health -= amt;

        if (health < 0)
        {
            health = 0;
        }
    }

    public void UpdateDisplay()
    {
        gameObject.GetComponent<StatsTextDisplay>().SetHealth(health);
        gameObject.GetComponent<StatsTextDisplay>().SetAttack(damage);
    }

    public void SetCharging(bool charge)
    {
        charging = charge;
    }

    public void ReleaseCharge()
    {
        damage = unchargedDamage;
        charging = false;
    }
}
