using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelection : MonoBehaviour
{ 
    [SerializeField] private int characterID;
    [SerializeField] private GameObject Manager; 

    private string lastSelectedUnitName; 

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterSelect()
    {
        //Debug.Log("Character intial position:  " + CharacterManager.allAlliedCharacters[characterID].name);
        //Debug.Log("Character intial position:  " + name);
        // int[] playerLoc = { CharacterManager.allAlliedCharacters[characterID].GetComponent<UnitCoordinates>().x, CharacterManager.allAlliedCharacters[characterID].GetComponent<UnitCoordinates>().y };


        //Manager.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: name, toHighlight: true, playerLocation: playerLoc);

        //  Manager.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: prevName, toHighlight: false, playerLocation: null);
        GetLastGameObjectSelected();
    }



    public EventSystem eventSystem; //select event system in editor
    private GameObject lastSelectedButton;
    private GameObject currentSelectedButton;
    private GameObject currentSelectedGameObject_Recent;


    private void GetLastGameObjectSelected()
    {
        if (eventSystem.currentSelectedGameObject != currentSelectedGameObject_Recent)
        {
            lastSelectedButton = currentSelectedGameObject_Recent;
            currentSelectedGameObject_Recent = eventSystem.currentSelectedGameObject;
            Debug.Log("lastSelectedGameObject:  " + lastSelectedButton);
            Debug.Log("currentSelectedGameObject_Recent:  " + currentSelectedGameObject_Recent);
        }
    }

}
