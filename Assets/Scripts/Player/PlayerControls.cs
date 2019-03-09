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
        playerLoc = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (DialoguePanelManager.playerControlsUnlocked && !TutorialCards.isTutorialRunning)
        {
            if (mouseClick && GameManager.isPlayerTurn && GameManager.HaveEnergy())
            {
                MoveOnClickedGridPiece();

                //Populates selectedUnitName and selectedUnit
                GetPlayer();

                AttackEnemy();

                /*GameObject character;

                int thisCharacterX = character.GetComponent<UnitCoordinates>().x;
                int thisCharacterY = character.GetComponent<UnitCoordinates>().y;*/

                if (piecesHighlighted)
                {
                    gameObject.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: lastSelectedUnitName, toHighlight: false, playerLocation: null);
                    piecesHighlighted = false;
                } 
                if (selectedUnit)
                {
                    gameObject.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: selectedUnitName, toHighlight: true, playerLocation: playerLoc);
                    GameObject.Find("GridX" + 1 + "Y" + 3).transform.GetChild(0).GetComponent<FreeSpaceHighlightAnim>().isVisible = true;
                    piecesHighlighted = true;                    
                }

                MouseClickToggle();
            }

            if (Input.GetMouseButtonDown(1))
            {
                ToggleStatVisibility();
            }
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
        RaycastHit hitCannon = RaycastManager.GetRaycastHitForTag("Cannon");
        RaycastHit hit = RaycastManager.GetRaycastHitForTag("Player");
        Transform moveLoc = GetComponent<GridPieceSelect>().GetGridPieceOnClick();

        if (hitCannon.transform == null && hit.transform == null && selectedUnit && moveLoc && moveLoc.GetComponent<GridPieceHighlight>().isHighlighted && GameManager.HaveEnergy())
        {
            GameObject playerCoords = GetComponent<GridPieceSelect>().GetGridPieceCoords(playerLoc[0], playerLoc[1]).gameObject;
            playerCoords.GetComponent<GridPiece>().unit = null;
            selectedUnit.position = moveLoc.transform.position;
            selectedUnit.GetComponent<UnitCoordinates>().SetUnitCoordinates(moveLoc.gameObject.GetComponent<GridCoordinates>().x, moveLoc.gameObject.GetComponent<GridCoordinates>().y);
            moveLoc.gameObject.GetComponent<GridPiece>().unit = selectedUnit.gameObject;
            GameManager.ReduceEnergy();
            Stats characterStats = selectedUnit.gameObject.GetComponent<Stats>();
            if (characterStats != null)
            {
                characterStats.GainMeter(1);
            }
        }
    }

    public void SetSelectedUnit(Transform newUnit)
    {
        selectedUnit = newUnit;
        if (selectedUnit != null)
        {
            playerLoc[0] = selectedUnit.GetComponent<UnitCoordinates>().x;
            playerLoc[1] = selectedUnit.GetComponent<UnitCoordinates>().y;
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
        RaycastHit hitCannon = RaycastManager.GetRaycastHitForTag("Cannon");
        RaycastHit hit = RaycastManager.GetRaycastHitForTag("Player");
        if (hitCannon.transform == null && hit.transform != null)
        {
            selectedUnit = hit.transform;
            playerLoc[0] = selectedUnit.GetComponent<UnitCoordinates>().x;
            playerLoc[1] = selectedUnit.GetComponent<UnitCoordinates>().y;
            selectedUnitName = hit.transform.name.Substring(1, hit.transform.name.IndexOf("_") - 1);

            //HUDCharacterHighlight.HighlightPortrait();
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
        RaycastHit hit = RaycastManager.GetRaycastHitForTag("Player");
        if (hit.transform == null)
        {
            hit = RaycastManager.GetRaycastHitForTag("Enemy");
        }
        if (hit.transform == null)
        {
            hit = RaycastManager.GetRaycastHitForTag("PirateBoss");
        }
        if (hit.transform != null)
        {
            clickedUnit = hit.transform;

            //find the objects and their children
            GameObject presenceObj = clickedUnit.gameObject.GetComponent<KeyObjectReferences>().uiAttackObject;
            GameObject presenceObjChild = presenceObj.transform.GetChild(0).gameObject;
            GameObject resistObj = clickedUnit.gameObject.GetComponent<KeyObjectReferences>().uiHealthObject;
            GameObject resistObjChild = resistObj.transform.GetChild(0).gameObject;
            //turn them both on if one is off
            if ((presenceObj.activeSelf && !resistObj.activeSelf) || (!presenceObj.activeSelf && resistObj.activeSelf))
            {
                presenceObj.SetActive(true);
                resistObj.SetActive(true);
                presenceObjChild.SetActive(true);
                resistObjChild.SetActive(true);
            }
            else
            {
                //set the parents to be the opposite of what they are
                presenceObj.SetActive(!presenceObj.activeSelf);
                resistObj.SetActive(!resistObj.activeSelf);
                //set the children to be equal to their parent
                presenceObjChild.SetActive(presenceObj.activeSelf);
                resistObjChild.SetActive(resistObj.activeSelf);
            }
        }
    }

    public void AttackEnemy()
    {
        RaycastHit hitEnemy = RaycastManager.GetRaycastHitForTag("Enemy");

        if (hitEnemy.transform == null)
        {
            return;
        }

        if (!CannonStaticVariables.isCannonSelected && prevSelectedUnit && !prevSelectedUnit.GetComponent<Stats>().hasAttacked)
        {
            if (AdjacencyHandler.CompareAdjacency(hitEnemy.transform.gameObject, prevSelectedUnit.gameObject, 1))
            {
                Stats enemyStats = hitEnemy.transform.gameObject.GetComponent<Stats>();
                enemyStats.TakeDamage(prevSelectedUnit.gameObject.GetComponent<Stats>().damage);
                enemyStats.CheckDeath();
                Debug.Log("Enemy taking damage!");
                prevSelectedUnit.GetComponent<Stats>().hasAttacked = true;
                prevSelectedUnit.GetComponent<Stats>().ReleaseCharge();
            }
        }
    }

    /*
    public void CheckSelection(CharacterAbility.selectionType selectType, string abilityName)
    {
        if (selectType == CharacterAbility.selectionType.enemy)
        {
            RaycastHit hitEnemy = GetComponent<RaycastManager>().GetRaycastHitForTag("Enemy");
            if (hitEnemy.transform != null)
            {
                if (AdjacencyHandler.CompareAdjacency(prevSelectedUnit.gameObject, hitEnemy.transform.gameObject, 2))
                {
                    CharacterAbility.ActivateFireball(prevSelectedUnit.gameObject, hitEnemy.transform.gameObject);
                }
            }
            else
            {
                inSelectionMode = false;
            }
        }
    }
    */
}