using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HallyEffectAnim : MonoBehaviour
{
    private int maxValue;
    // Use this for initialization
    void Start()
    {
        maxValue = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterManager.allAlliedCharacters[1] && CharacterManager.allAlliedCharacters[1].GetComponent<Stats>().meterUnitsFilled == maxValue)
        {
            gameObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
        }

    }
}
