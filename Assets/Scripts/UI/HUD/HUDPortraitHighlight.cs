using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDPortraitHighlight : MonoBehaviour
{
    [SerializeField] private int characterID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PanelHighlight()
    {
        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            //if (CharacterManager.allAlliedCharacters[i].name == )
        }        
    }
}
