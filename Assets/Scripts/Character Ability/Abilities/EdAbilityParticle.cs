using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdAbilityParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem poison;
    ParticleSystem.MainModule mainModule;
    ParticleSystem.EmissionModule emissionModule;
    Color lightGreen;
    Color deepGreen;

    bool isSmokeOn;
    float timer;
    [SerializeField] float poisonDuration;
    

    // Start is called before the first frame update
    void Start()
    {
        poison.Stop();
        mainModule = poison.main;
        emissionModule = poison.emission;
        lightGreen = new Vector4(0, 255, 0, 150);
        deepGreen = new Vector4(0, 255, 0, 255);
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
        mainModule.startColor = lightGreen;
        emissionModule.rateOverTime = 60;
        mainModule.startLifetime = 5.0f;
        mainModule.startSpeed = 0.2f;
    }

    void SetParticleToUse()
    {
        mainModule.startColor = deepGreen;
        mainModule.startLifetime = 1.5f;
        mainModule.startSpeed = .8f;
        emissionModule.rateOverTime = 1000;
    }
}