using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            if (CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().health  <= 0)
            {

            }
        }
    }


    void Character()
    {

    }
}
