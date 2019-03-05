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

    private int characterID;
    private int index;
    private int[] playerLoc;

    private string[] characters;
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
            //Debug.Log("characters[index]:     " + characters[index]);
            //Debug.Log("currentSelectedButton.name:     " + currentSelectedButton.name);
            if (characters[index] == currentSelectedButton.name)
            {
                characterID = index; 
            }
        }   

        Debug.Log("characterID:     " + characterID);   

        //Mark: Disable the highlight space.
        if (lastSelectedButton != null)
        { 
            Manager.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: lastSelectedButton.name, toHighlight: false, playerLocation: null);
            Debug.Log("lastSelectedButton.name:     " + lastSelectedButton.name);
        }

        //Mark: Enable the highlight space
        if (currentSelectedButton != null)
        {
            playerLoc[0] = CharacterManager.allAlliedCharacters[characterID].GetComponent<UnitCoordinates>().x;
            playerLoc[1] = CharacterManager.allAlliedCharacters[characterID].GetComponent<UnitCoordinates>().y;
            Manager.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: characters[characterID], toHighlight: true, playerLocation: playerLoc);
            //Manager.GetComponent<PlayerControls>().SetSelectedUnit(currentSelectedButton.transform);
        }


    }
}
