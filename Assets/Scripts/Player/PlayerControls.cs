using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    private string selectedUnitName;
    private string lastSelectedUnitName;
    private Transform selectedUnit;
    private bool piecesHighlighted;
    private int[] playerLoc;
    private bool mouseClick;

    // Start is called before the first frame update
    void Start()
    {
        selectedUnitName = "NoUnitSelected";
        lastSelectedUnitName = "NoUnitSelected";

    }

    // Update is called once per frame
    void Update()
    {
        if (mouseClick)
        {

            moveOnClickedGridPiece();

            //Populates selectedUnitName and selectedUnit
            getPlayer();

            if (piecesHighlighted)
            {
                gameObject.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: lastSelectedUnitName, toHighlight: false);
                piecesHighlighted = false;
            }


            if (selectedUnit)
            {
                gameObject.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: selectedUnitName, toHighlight : true);
                piecesHighlighted = true;
            }

            mouseClickToggle();
        }
    }

    public void mouseClickToggle()
    {
        mouseClick = !mouseClick;
    }

    public string getSelectedUnitName()
    {
        return selectedUnitName;
    }

    private void moveOnClickedGridPiece()
    {
        int[] moveCoords = gameObject.GetComponent<GridPieceSelect>().getGridPieceCoordsOnClick();
        GameObject moveLoc = GameObject.Find("GridX" + moveCoords[0] + "Y" + moveCoords[1]);

        if (selectedUnit && moveLoc && moveLoc.GetComponent<GridPieceHighlight>().isHighlighted)
        {
            Debug.Log(moveLoc.transform.position);
            GameObject.Find("GridX" + playerLoc[0] + "Y" + playerLoc[1]).GetComponent<GridPiece>().unit = null;
            selectedUnit.position = moveLoc.transform.position;
            moveLoc.GetComponent<GridPiece>().unit = selectedUnit.gameObject;
        }
    }

    //Check hitTargets to see if we clicked on a player tag
    //Store the transform of the selected player
    //Pull the player name substring from the GameObject name
    //If we click on anything that's not a player, we unselect the last selected player
    private void getPlayer()
    {
        lastSelectedUnitName = selectedUnitName;
        RaycastHit hit = GetComponent<RaycastManager>().getRaycastHitForTag("Player");
        if (hit.transform != null)
        {
            playerLoc = gameObject.GetComponent<GridPieceSelect>().getGridPieceCoordsOnClick();
            selectedUnit = hit.transform;
            selectedUnitName = hit.transform.name.Substring(1, hit.transform.name.IndexOf("_") - 1);
        }
        else
        {
            selectedUnit = null;
            selectedUnitName = "NoUnitSelected";
        }

    }

}
