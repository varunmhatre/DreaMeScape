using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JadeEffectAnim : MonoBehaviour
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
        //for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        //{
        //    Debug.Log("Meter filled:    " + CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().meterUnitsFilled);
        //    if (CharacterManager.allAlliedCharacters[2].GetComponent<Stats>().meterUnitsFilled == maxValue)
        //    {
        //        Debug.Log("Activate the effecct");
        //        gameObject.GetComponent<Image>().enabled = true;
        //    }
        //    else
        //    {
        //        Debug.Log("Disable the effect");
        //        gameObject.GetComponent<Image>().enabled = false;
        //    }
        //}

        if (CharacterManager.allAlliedCharacters[2] && CharacterManager.allAlliedCharacters[2].GetComponent<Stats>().meterUnitsFilled == maxValue)
        {
            gameObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
        }

    }
}
