using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    private int healthValue;
    private int deathValue;
    // Start is called before the first frame update
    void Start()
    {
        deathValue = 0;
    }
    // Update is called once per frame
    void Update()
    {
        EnableDeathPanel();
    }

    void EnableDeathPanel()
    {
        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            healthValue = CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().health;  
                
            if (healthValue <= deathValue)
            {
                Debug.Log("Enable the death panel");
            }
        }
    }
}
