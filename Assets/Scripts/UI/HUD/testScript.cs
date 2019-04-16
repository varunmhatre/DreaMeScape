using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class testScript : MonoBehaviour
{ 
    public EventSystem eventSystem; //select event system in editor
    private GameObject lastSelectedButton;
    private GameObject currentSelectedButton;
    private GameObject currentSelectedGameObject_Recent;
    //[SerializeField] private int characterID; 

    [SerializeField] private GameObject Manager;

    //[SerializeField] private int characterID;
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
        GetLastGameObjectSelected(); 
    }
    private void GetLastGameObjectSelected()
    {
        int currentID = transform.GetComponent<CharacterID>().clickID;
        
        if (eventSystem.currentSelectedGameObject != currentSelectedGameObject_Recent)
        {
            lastSelectedButton = currentSelectedGameObject_Recent;            
            currentSelectedGameObject_Recent = eventSystem.currentSelectedGameObject;
        }
        //int[] playerLoc = { CharacterManager.allAlliedCharacters[characterID].GetComponent<UnitCoordinates>().x, CharacterManager.allAlliedCharacters[characterID].GetComponent<UnitCoordinates>().y };
        //Manager.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: currentSelectedGameObject_Recent.name, toHighlight: true, playerLocation: playerLoc);
               
        if (lastSelectedButton != null)
        {
           // Manager.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: lastSelectedButton.name, toHighlight: false, playerLocation: null);
        }
        
        
    }
}
