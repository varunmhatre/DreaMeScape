using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private string selectedUnitName;
    private string lastSelectedUnitName;
    public static Transform selectedUnit;
    public static Transform prevSelectedUnit;
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
        if (DialoguePanelManager.playerControlsUnlocked)
        {
            if (mouseClick && GetComponent<GameManager>().isPlayerTurn)
            {
                MoveOnClickedGridPiece();

                //Populates selectedUnitName and selectedUnit
                GetPlayer();

                AttackEnemy();

                if (piecesHighlighted)
                {
                    gameObject.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: lastSelectedUnitName, toHighlight: false);
                    piecesHighlighted = false;
                }

                if (selectedUnit)
                {
                    gameObject.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: selectedUnitName, toHighlight: true);
                    piecesHighlighted = true;
                }

                MouseClickToggle();
            }

            if (Input.GetMouseButtonDown(1))
            {
                ToggleStatVisibility();
            }

            mouseClickToggle();
        }
    }
    public void MouseClickToggle()
    {
        mouseClick = !mouseClick;
    }

    public string GetSelectedUnitName()
    {
        return selectedUnitName;
    }

    private void MoveOnClickedGridPiece()
    {
        int[] moveCoords = gameObject.GetComponent<GridPieceSelect>().GetGridPieceCoordsOnClick();
        GameObject moveLoc = GameObject.Find("GridX" + moveCoords[0] + "Y" + moveCoords[1]);

        if (selectedUnit && moveLoc && moveLoc.GetComponent<GridPieceHighlight>().isHighlighted && GameManager.HaveEnergy())
        {
            GameObject.Find("GridX" + playerLoc[0] + "Y" + playerLoc[1]).GetComponent<GridPiece>().unit = null;
            selectedUnit.position = moveLoc.transform.position;
            selectedUnit.GetComponent<UnitCoordinates>().SetUnitCoordinates(moveCoords[0], moveCoords[1]);
            moveLoc.GetComponent<GridPiece>().unit = selectedUnit.gameObject;
            GameManager.ReduceEnergy();
            Stats characterStats = selectedUnit.gameObject.GetComponent<Stats>();
            if (characterStats != null)
            {
                characterStats.GainMeter(1);
            }
        }
    }

    //Check hitTargets to see if we clicked on a player tag
    //Store the transform of the selected player
    //Pull the player name substring from the GameObject name
    //If we click on anything that's not a player, we unselect the last selected player
    private void GetPlayer()
    {
        lastSelectedUnitName = selectedUnitName;
        prevSelectedUnit = selectedUnit;
        RaycastHit hit = GetComponent<RaycastManager>().GetRaycastHitForTag("Player");
        if (hit.transform != null)
        {
            playerLoc = gameObject.GetComponent<GridPieceSelect>().GetGridPieceCoordsOnClick();
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

            //find the objects and their children
            GameObject presenceObj = clickedUnit.gameObject.GetComponent<KeyObjectReferences>().uiAttackObject;
            GameObject presenceObjChild = presenceObj.transform.GetChild(0).gameObject;
            GameObject resistObj = clickedUnit.gameObject.GetComponent<KeyObjectReferences>().uiHealthObject;
            GameObject resistObjChild = resistObj.transform.GetChild(0).gameObject;
            //set the parents to be the opposite of what they are
            presenceObj.SetActive(!presenceObj.activeSelf);
            resistObj.SetActive(!resistObj.activeSelf);
            //set the children to be equal to their parent
            presenceObjChild.SetActive(presenceObj.activeSelf);
            resistObjChild.SetActive(resistObj.activeSelf);
        }
    }

    public void AttackEnemy()
    {
        RaycastHit hitEnemy = GetComponent<RaycastManager>().GetRaycastHitForTag("Enemy");
        RaycastHit hitBoss = GetComponent<RaycastManager>().GetRaycastHitForTag("Boss");

        bool hittingBoss = false;

        if (hitEnemy.transform != null)
        {
            hittingBoss = false;
        }
        else if (hitBoss.transform != null)
        {
            hittingBoss = true;
        }
        else if (hitBoss.transform == null && hitEnemy.transform == null)
        {
            return;
        }

        if (!CannonStaticVariables.isCannonSelected && prevSelectedUnit && prevSelectedUnit.GetComponent<Stats>().hasAttacked == false && GameManager.currentEnergy > 0)
        {
            if (hittingBoss && AdjacencyHandler.CompareAdjacency(hitBoss.transform.gameObject, prevSelectedUnit.gameObject, 1))
            {
                Stats bossStats = hitBoss.transform.gameObject.GetComponent<Stats>();
                bossStats.TakeDamage(prevSelectedUnit.gameObject.GetComponent<Stats>().damage);
                prevSelectedUnit.GetComponent<Stats>().hasAttacked = true;
                GameManager.currentEnergy--;
            }
            else if (!hittingBoss && AdjacencyHandler.CompareAdjacency(hitEnemy.transform.gameObject, prevSelectedUnit.gameObject, 1))
            {
                Stats enemyStats = hitEnemy.transform.gameObject.GetComponent<Stats>();
                enemyStats.TakeDamage(prevSelectedUnit.gameObject.GetComponent<Stats>().damage);
                prevSelectedUnit.GetComponent<Stats>().hasAttacked = true;
                GameManager.currentEnergy--;
            }
        }
    }
}