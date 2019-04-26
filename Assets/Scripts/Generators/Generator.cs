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

    [SerializeField] private GameObject genObj;
    [SerializeField] private GameObject leverObj;

    [SerializeField] public SpriteRenderer baseVisual;
    [SerializeField] public Sprite onVisual;
    [SerializeField] public Sprite offVisual;

    private bool hasSwapped;

    // Use this for initialization
    void Start()
    {
        hasSwapped = false;
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
            baseVisual.sprite = offVisual;
        }
        else
        {
            baseVisual.sprite = onVisual;
        }
        //this segment is under repair
        /*
        else if (gameObject.transform.GetChild(0).GetComponent<Animator>() != null)
        {
            if (!hasSwapped)
            {
                gameObject.transform.GetChild(0).GetComponent<Animator>().enabled = true;
                hasSwapped = true;
            }
            genObj.GetComponent<Animator>().applyRootMotion = true;
            genObj.GetComponent<Animator>().applyRootMotion = false;
            leverObj.transform.Rotate(0.0f, 0.0f, 270.0f);
            genObj.GetComponent<Animator>().applyRootMotion = true;
        }
        */
        //end segment

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