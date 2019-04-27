using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    private string selectedUnitName;
    private string lastSelectedUnitName;
    public static Transform selectedUnit;
    public static Transform prevSelectedUnit;
    public static bool clearSelectedUnit;
    private bool piecesHighlighted;
    private int[] playerLoc;
    private bool startSliding;
    private Vector3 toMoveLoc;
    private float slideSpeed;

    // Start is called before the first frame update
    void Start()
    {
        slideSpeed = 10.0f;
        startSliding = false;
        clearSelectedUnit = false;
        selectedUnitName = "NoUnitSelected";
        lastSelectedUnitName = "NoUnitSelected";
        playerLoc = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (DialoguePanelManager.playerControlsUnlocked && TutorialCards.isTutorialRunning || SceneManager.GetActiveScene().name == "TutorialScene")
        {
            if (RaycastManager.leftClicked && GameManager.isPlayerTurn && GameManager.HaveEnergy() && !CharacterAbility.inSelectionMode && !CannonStaticVariables.isCannonSelected)
            {
                MoveOnClickedGridPiece();

                //Populates selectedUnitName and selectedUnit
                GetPlayer();

                AttackEnemy();

                if (piecesHighlighted)
                {
                    gameObject.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: lastSelectedUnitName, toHighlight: false, playerLocation: null);
                    piecesHighlighted = false;
                } 
                if (selectedUnit && !clearSelectedUnit)
                {
                    gameObject.GetComponent<GridPieceSelect>().highlightMoveSpaces(playerName: selectedUnitName, toHighlight: true, playerLocation: playerLoc);
                    piecesHighlighted = true;                    
                }
            }

            if (RaycastManager.rightClicked)
            {
                ToggleStatVisibility();
            }

            if (startSliding && prevSelectedUnit != null)
            {
                SlideMovedCharacter(prevSelectedUnit.gameObject, prevSelectedUnit.position, toMoveLoc);
            }
            else if (startSliding && prevSelectedUnit == null)
            {
                
            }
        }
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
            //selectedUnit.position = moveLoc.transform.position;
            startSliding = true;
            toMoveLoc = moveLoc.position;
            selectedUnit.GetComponent<UnitCoordinates>().SetUnitCoordinates(moveLoc.gameObject.GetComponent<GridCoordinates>().x, moveLoc.gameObject.GetComponent<GridCoordinates>().y);
            moveLoc.gameObject.GetComponent<GridPiece>().unit = selectedUnit.gameObject;
            GameManager.ReduceEnergy();
           // EnergyUsedAnimation.enableAnim = true;
            Stats characterStats = selectedUnit.gameObject.GetComponent<Stats>();
            if (characterStats != null)
            {
                characterStats.GainMeter(1);
                characterStats.HealthMeter(1);
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
        Transform savedPrev = prevSelectedUnit;
        prevSelectedUnit = selectedUnit;
        RaycastHit hitCannon = RaycastManager.GetRaycastHitForTag("Cannon");
        RaycastHit hit = RaycastManager.GetRaycastHitForTag("Player");
        if (hitCannon.transform == null && hit.transform != null)
        {
            selectedUnit = hit.transform;
            playerLoc[0] = selectedUnit.GetComponent<UnitCoordinates>().x;
            playerLoc[1] = selectedUnit.GetComponent<UnitCoordinates>().y;
            selectedUnitName = hit.transform.name.Substring(1, hit.transform.name.IndexOf("_") - 1);


            //if currently animating, finish doing so
            if (startSliding && selectedUnit && savedPrev)
            {
                startSliding = false;
                savedPrev.transform.position = toMoveLoc;
            }

            //HUDCharacterHighlight.HighlightPortrait();
        }
        else
        {
            clearSelectedUnit = true;
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
                prevSelectedUnit.GetComponent<Stats>().hasAttacked = true;
                prevSelectedUnit.GetComponent<Stats>().ReleaseCharge();
            }
        }
    }

    public void SlideMovedCharacter(GameObject character, Vector3 start, Vector3 goal)
    {
        Vector3 toGo = goal - start;
        Vector3 direction = toGo.normalized;
        Vector3 toMove = direction * slideSpeed;

        //Debug.Log(toMove.magnitude);

        character.transform.position += toMove * Time.deltaTime;

        Vector3 newToGo = goal - character.transform.position;
        Vector3 newDirection = newToGo.normalized;

        //check if you've gone too far
        if (direction != newDirection)
        {
            //Debug.Log("You've gone too far!");
            character.transform.position = goal;
            startSliding = false;
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