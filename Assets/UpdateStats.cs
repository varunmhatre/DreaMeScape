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
        foreach (var item in CharacterManager.allAlliedCharacters)
        {
            for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
            {
                healthValue = CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().health;
                presenceTextArr[i].text = healthValue.ToString();

                attackValue = CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().damage;
                resistTextArr[i].text = attackValue.ToString();
            }
        }
    }
}
