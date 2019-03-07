using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public bool isOn;
    [SerializeField] TextMesh generatorPopupText;
    private int numCharsToSurround;
    private int energyProvides;
    private bool provisionAdded;

    // Use this for initialization
    void Start()
    {
        provisionAdded = false;
        isOn = false;
        energyProvides = 2;
        numCharsToSurround = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn)
        {
            CheckToTurnOn();
            UpdateTextValue();
        }

        if (isOn && !provisionAdded)
        {
            ProvideEnergy(energyProvides);
            provisionAdded = true;
        }
    }

    private void CheckToTurnOn()
    {
        if (AdjacencyHandler.NumPlayerCharactersAround(gameObject, 1) >= numCharsToSurround)
        {
            isOn = true;
        }
    }

    private void ProvideEnergy(int amt)
    {
        GameManager.totalEnergy += amt;
    }

    private void ChangeText(int value)
    {
        generatorPopupText.text = value.ToString();
        if (value == -1)
        {
            generatorPopupText.text = "!!!";
        }
    }

    private void UpdateTextValue()
    {
        int result = numCharsToSurround - AdjacencyHandler.NumPlayerCharactersAround(gameObject, 1);
        if (result <= 0)
        {
            result = -1;
        }
        ChangeText(result);
    }
}