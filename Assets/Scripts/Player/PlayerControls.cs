using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    private string selectedUnitName;
    private string lastSelectedUnitName;
    private Transform selectedUnit;
    private bool piecesHighlighted;
    // Start is called before the first frame update
    void Start()
    {
        selectedUnitName = "NoUnitSelected";
        lastSelectedUnitName = "NoUnitSelected";

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
            
        }
    }

    public string getSelectedUnitName()
    {
        return selectedUnitName;
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
