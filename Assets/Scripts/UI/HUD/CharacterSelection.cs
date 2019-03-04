using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    //[SerializeField] private Button[] characters;
    [SerializeField] private int clickID;
   // [SerializeField] private GameObject playerControls;

    private string selectedUnitName; 
    private bool piecesHighlighted;
    private int[] playerLoc;


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
        Debug.Log("ClickID:      " + clickID);
        

       // gameObject.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: selectedUnitName, toHighlight: true, playerLocation: playerLoc);
       // piecesHighlighted = true;
    } 
}
