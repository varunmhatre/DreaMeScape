using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdAbilityParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem poison;
    ParticleSystem.MainModule mainModule;
    ParticleSystem.EmissionModule emissionModule;
    Color lightPurple;
    Color deepPurple;

    bool isSmokeOn;
    float timer;
    [SerializeField] float poisonDuration;
    

    // Start is called before the first frame update
    void Start()
    {
        poison.Stop();
        mainModule = poison.main;
        emissionModule = poison.emission;
        lightPurple = new Vector4(183, 0, 225, 150);
        deepPurple = new Vector4(120, 0, 221, 255);
    }

    private void Update()
    {
        if (isSmokeOn)
        {
            timer += Time.deltaTime;
            if (timer >= poisonDuration)
            {
                timer = 0;
                isSmokeOn = false;
                poison.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }

    public void IsHovering()
    {
        SetParticleToHover();
        poison.Play();
    }

    public void StoppedHovering()
    {
        poison.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void Clicked()
    {
        SetParticleToUse();
        isSmokeOn = true;
    }

    void SetParticleToHover()
    {
        mainModule.startColor = lightPurple;
        emissionModule.rateOverTime = 100;
        mainModule.startLifetime = 5.0f;
        mainModule.startSpeed = 0.2f;
    }

    void SetParticleToUse()
    {
        mainModule.startColor = deepPurple;
        mainModule.startLifetime = 1.5f;
        mainModule.startSpeed = .8f;
        emissionModule.rateOverTime = 1000;
    }
}