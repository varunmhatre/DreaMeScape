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

            MoveOnClickedGridPiece();

            //Populates selectedUnitName and selectedUnit
            GetPlayer();

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

        if (Input.GetMouseButtonDown(1))
        {
            ToggleStatVisibility();
        }
    }

    public string GetSelectedUnitName()
    {
        return selectedUnitName;
    }

    private void MoveOnClickedGridPiece()
    {
        int[] moveCoords = gameObject.GetComponent<GridPieceSelect>().getGridPieceCoordsOnClick();
        GameObject moveLoc = GameObject.Find("GridX" + moveCoords[0] + "Y" + moveCoords[1]);

        if (selectedUnit && moveLoc && moveLoc.GetComponent<GridPieceHighlight>().isHighlighted)
        {
            Debug.Log(moveLoc.transform.position);
            selectedUnit.position = moveLoc.transform.position;
        }
    }

    //Check hitTargets to see if we clicked on a player tag
    //Store the transform of the selected player
    //Pull the player name substring from the GameObject name
    //If we click on anything that's not a player, we unselect the last selected player
    private void GetPlayer()
    {
        lastSelectedUnitName = selectedUnitName;
        RaycastHit hit = GetComponent<RaycastManager>().GetRaycastHitForTag("Player");
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

    private void ToggleStatVisibility()
    {
        Transform clickedUnit = null;
        RaycastHit hit = GetComponent<RaycastManager>().GetRaycastHitForTag("Player");
        if (hit.transform == null)
        {
            hit = GetComponent<RaycastManager>().GetRaycastHitForTag("Enemy");
        }
        if (hit.transform == null)
        {
            hit = GetComponent<RaycastManager>().GetRaycastHitForTag("PirateBoss");
        }
        if (hit.transform != null)
        {
            clickedUnit = hit.transform;

            Debug.Log(clickedUnit);

            //find the objects and their children
            GameObject presenceObj = clickedUnit.gameObject.GetComponent<KeyObjectReferences>().uiPresenceObject;
            GameObject presenceObjChild = presenceObj.transform.GetChild(0).gameObject;
            GameObject resistObj = clickedUnit.gameObject.GetComponent<KeyObjectReferences>().uiResistObject;
            GameObject resistObjChild = resistObj.transform.GetChild(0).gameObject;
            //set the parents to be the opposite of what they are
            presenceObj.SetActive(!presenceObj.activeSelf);
            resistObj.SetActive(!resistObj.activeSelf);
            //set the children to be equal to their parent
            presenceObjChild.SetActive(presenceObj.activeSelf);
            resistObjChild.SetActive(resistObj.activeSelf);
        }
    }

}
