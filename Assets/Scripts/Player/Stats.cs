using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] public int presence;
    [SerializeField] public int unchargedDamage;
    [SerializeField] public int maxHealth;

    public DamageEffects dmgEff;
    public bool statsVisible;
    public bool hasAttacked;
    private bool charging;

    public int meterUnitsFilled;
    public int healthFilled;
    [SerializeField] public int maxMeter;

    public bool isEncumbered;
    [SerializeField] public bool isEnemy;
    
    private void Start()
    {
        isEncumbered = false;
        if (GameObject.FindGameObjectWithTag("DamageEffect") != null)
        {
            dmgEff = GameObject.FindGameObjectWithTag("DamageEffect").GetComponent<DamageEffects>();
        }
    }

    public void GainMeter(int amt)
    {        
        meterUnitsFilled += amt;
        if (meterUnitsFilled > maxMeter)
        {
            meterUnitsFilled = maxMeter;
        }
    }

    public void HealthMeter(int value)
    {
        healthFilled += value;

        if(healthFilled < maxHealth)
        {
            healthFilled = maxHealth;
        }
    }

    private void Update()
    {
        UpdateDisplay();
    }

    public void Encumber()
    {
        isEncumbered = true;
        damage /= 2;
        health /= 3;
        CheckDeath();
    }

    public void EmptyMeter()
    {
        meterUnitsFilled = 0;
    }

    public void Die()
    {
        if (gameObject.GetComponent<PirateCaptain>())
            PirateCaptain.isPirateDefeated = true;
        Destroy(gameObject);
    }

    public void CheckDeath()
    {
        if (health <= 0)
        {
            if (isEnemy)
            {
                CharacterManager.RemoveFromEnemies(gameObject);
            }
            else
            {
                CharacterManager.RemoveFromAllies(gameObject);
            }

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

        if (dmgEff)
        {
            dmgEff.SendToLocation(dmgEff.currIndex, gameObject, amt);
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
