using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStats : MonoBehaviour
{
    [SerializeField] Text[] presenceTextArr;
    [SerializeField] Text[] resistTextArr;

    private int healthValue;
    private int attackValue;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            if (!CharacterManager.allAlliedCharacters[i])
                continue;
            healthValue = CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().health;
            resistTextArr[i].text = healthValue.ToString();

            attackValue = CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().damage;
            presenceTextArr[i].text = attackValue.ToString();
        }
    }
}
