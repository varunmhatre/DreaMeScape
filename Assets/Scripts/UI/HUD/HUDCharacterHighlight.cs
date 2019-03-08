using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDCharacterHighlight : MonoBehaviour
{
    [SerializeField] private GameObject[] character;

    private static string[] characters;
    private static int[] characterID;
    private static int index;
    private static int clickID;
    // Start is called before the first frame update
    void Start()
    {
        characters = new string[] { "Ed", "Hally", "Jade", "Kent", "Meda" };
        characterID = new int[5];

        for (int i = 0; i < characters.Length; i++)
        {
            characterID[i] = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void HighlightPortrait()
    {
        Debug.Log("ClickID:    " + clickID);
        for (index = 0; index < CharacterManager.allAlliedCharacters.Count; index++)
        {
            if (characters[index] == CharacterManager.allAlliedCharacters[index].name)
            {
                clickID = index;
                
            }
        }
    }
}
