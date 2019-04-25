using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectCharacterHUD : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject Manager;

    private GameObject lastSelectedButton;
    private GameObject currentSelectedButton;

    private int clickID;
    private int index;
    private int[] playerLoc;

    private string[] characters;
    private int[] characterID;

    [SerializeField] private GameObject[] mainCharacter;
    private enum CharacterType
    {
        Ed, Hally, Kent, Jade, Meda
    }

    //[SerializeField] private int characterID; 

    // Start is called before the first frame update
    void Start()
    {        
        characters = new string[]{"Ed", "Hally", "Jade", "Kent", "Meda"};
        playerLoc = new int[2];

        characterID = new int[5];

        for(int i = 0; i < characters.Length; i++)
        {
            characterID[i] = i;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    public void CharacterSelect()
    {        
        if (eventSystem.currentSelectedGameObject != currentSelectedButton)
        {
            lastSelectedButton = currentSelectedButton;
            currentSelectedButton = eventSystem.currentSelectedGameObject; 
        }
        for (index = 0; index < CharacterManager.allAlliedCharacters.Count; index++)
        {
            if (!CharacterManager.allAlliedCharacters[index])
                continue;
            if (characters[index] == currentSelectedButton.name)
            {
                clickID = index; 
            }
        }   
        //Mark: Disable the highlight space.
        if (lastSelectedButton != null)
        { 
            Manager.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: lastSelectedButton.name, toHighlight: false, playerLocation: null);
        }

        //Mark: Enable the highlight space
        if (currentSelectedButton != null)
        {
            //playerLoc[0] = CharacterManager.allAlliedCharacters[clickID].GetComponent<UnitCoordinates>().x;
            //playerLoc[1] = CharacterManager.allAlliedCharacters[clickID].GetComponent<UnitCoordinates>().y;

            playerLoc[0] = mainCharacter[clickID].GetComponent<UnitCoordinates>().x;
            playerLoc[1] = mainCharacter[clickID].GetComponent<UnitCoordinates>().y;

            Manager.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: characters[clickID], toHighlight: true, playerLocation: playerLoc);
            
            Manager.GetComponent<PlayerControls>().SetSelectedUnit(mainCharacter[clickID].transform);
        }


    }
}
