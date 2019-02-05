using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    private string selectedUnitName;
    private Transform selectedUnit;

    // Start is called before the first frame update
    void Start()
    {
        selectedUnitName = "NoUnitSelected";

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Populates selectedUnitName and selectedUnit
            getPlayer();
            if(selectedUnit)
            {                
                
                gameObject.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: selectedUnitName);
            }
            else
            {
                //clear highlight
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
        RaycastHit hit = GetComponent<RaycastManager>().getRaycastHitForTag("Player");
        if (hit.transform != null)
        {
            selectedUnit = hit.transform;
            selectedUnitName = hit.transform.name.Substring(1, hit.transform.name.IndexOf("_") - 1);
            Debug.Log(selectedUnitName);
        }
        else
        {
            selectedUnit = null;
            selectedUnitName = "NoUnitSelected";
        }

    }

}
