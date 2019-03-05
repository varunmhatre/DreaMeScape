using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectCharacterHUD : MonoBehaviour
{ 
    [SerializeField]private EventSystem eventSystem;
    [SerializeField] private GameObject Manager;

    private GameObject lastSelectedButton;
    private GameObject currentSelectedButton;

    private int[] characterID;
    
    //[SerializeField] private int characterID; 
     
    // Start is called before the first frame update
    void Start()
    {
      //  for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            //Debug.Log("CharacterManager.allAlliedCharacters:        " + CharacterManager.allAlliedCharacters[i]);
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

            Debug.Log("currentSelectedGameObject_Recent:  " + currentSelectedButton.name);
        }

        //Mark: Enable the highlight space
        //int[] playerLoc = { CharacterManager.allAlliedCharacters[characterID].GetComponent<UnitCoordinates>().x, CharacterManager.allAlliedCharacters[characterID].GetComponent<UnitCoordinates>().y };
        //Manager.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: currentSelectedGameObject_Recent.name, toHighlight: true, playerLocation: playerLoc);


        //Mark: Disable the highlight space.
        if (lastSelectedButton != null)
        {
            Debug.Log("lastSelectedGameObject:  " + lastSelectedButton.name);
            // Manager.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: lastSelectedButton.name, toHighlight: false, playerLocation: null);
        }
    }
    private void GetLastGameObjectSelected()
    { 
        
    }
}
