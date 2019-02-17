using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] int presence;

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
}
