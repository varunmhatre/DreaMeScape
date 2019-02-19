using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] int presence;
    public bool statsVisible;
    public bool hasAttacked;

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
}
