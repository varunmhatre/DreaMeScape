using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterAbility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private int amountMeterNeeded;
    [SerializeField] private int buttonId;
    private bool isInteractable;
    private string currAbilityName;
    public static bool inSelectionMode;
    private bool justClickedButton;

    public enum selectionType
    {
        ally,
        enemy,
        emptySpace
    }

    private selectionType currSelectionType;

    // Start is called before the first frame update
    void Start()
    {
        amountMeterNeeded = 5;
        isInteractable = false;
        inSelectionMode = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfMeterFull(buttonId, CharacterManager.allAlliedCharacters[buttonId].GetComponent<Stats>()))
        {            
            isInteractable = true;
        }

        if (inSelectionMode)
        {
            CheckSelection(currSelectionType, currAbilityName);
        }
        
        gameObject.GetComponent<Button>().interactable = isInteractable;
        justClickedButton = false;
    }

    public void ActivateAbility()
    {
        transform.GetComponent<Image>().color = Color.blue;
        Stats charStats = CharacterManager.allAlliedCharacters[buttonId].GetComponent<Stats>();
        
        if (buttonId == 4 && GameManager.currentEnergy >= 1)
        {
            TryFireball(charStats.gameObject);
        }
        else if (buttonId == 3 && GameManager.currentEnergy >= 1)
        {
            ActivateCleave(charStats.gameObject);
            //for testing Henry's ability with Kent
            //ActivateBolster(charStats.gameObject);
        }
        else if (buttonId == 2 && GameManager.currentEnergy >= 1)
        {
            TrySprint(charStats.gameObject);
        }
        else if (buttonId == 1 && GameManager.currentEnergy >= 1)
        {
            //Hally
        }
        else if (buttonId == 0 && GameManager.currentEnergy >= 1)
        {
            ActivateParalyzingPotion(charStats.gameObject);
        }
    }

    public bool CheckIfMeterFull(int id, Stats charStats)
    {
        if (buttonId == id && charStats.meterUnitsFilled >= amountMeterNeeded)
        {
            return true;
        }

        return false;
    }
    
    public void CheckSelection(selectionType selectType, string abilityName)
    {
        if (selectType == selectionType.enemy)
        {
            if (abilityName == "fireball")
            {
                if (Input.GetMouseButtonDown(0) && !justClickedButton)
                {
                    RaycastHit hitEnemy = RaycastManager.GetRaycastHitForTag("Enemy");
                    if (hitEnemy.transform != null)
                    {
                        if (AdjacencyHandler.CompareAdjacency(CharacterManager.allAlliedCharacters[buttonId], hitEnemy.transform.gameObject, 2))
                        {
                            ActivateFireball(CharacterManager.allAlliedCharacters[buttonId], hitEnemy.transform.gameObject);
                        }
                    }
                    else
                    {
                        inSelectionMode = false;
                    }
                }
            }
        }
        else if (selectType == selectionType.emptySpace)
        {
            if (abilityName == "sprint")
            {
                if (Input.GetMouseButtonDown(0) && !justClickedButton)
                {
                    Debug.Log("Preparing to sprint");
                    RaycastHit hitPiece = RaycastManager.GetRaycastHitForTag("GridPiece");
                    if (hitPiece.transform != null)
                    {
                        Debug.Log("Moving to a space.");
                        if (AdjacencyHandler.CompareAdjacency(CharacterManager.allAlliedCharacters[buttonId], hitPiece.transform.gameObject, 2))
                        {
                            ActivateSprint(CharacterManager.allAlliedCharacters[buttonId], hitPiece.transform.gameObject);
                        }
                    }
                    else
                    {
                        inSelectionMode = false;
                    }
                }
            }
        }
    }

    //For Henry when we add him
    public void ActivateBolster(GameObject character)
    {
        bool buffSomeone = false;
        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            GameObject ally = CharacterManager.allAlliedCharacters[i];
            if (AdjacencyHandler.CompareAdjacency(character, ally, 2))
            {
                ally.GetComponent<Stats>().GetComponent<Stats>().GainMeter(3);
                buffSomeone = true;
            }
        }
        if (buffSomeone)
        {
            GameManager.currentEnergy--;
            character.GetComponent<Stats>().EmptyMeter();
            isInteractable = false;
        }
    }

    public void ActivateParalyzingPotion(GameObject character)
    {
        bool stunSomeone = false;
        for (int i = 0; i < CharacterManager.allEnemyCharacters.Count; i++)
        {
            GameObject enemy = CharacterManager.allEnemyCharacters[i];
            if (AdjacencyHandler.CompareAdjacency(character, enemy, 2))
            {
                enemy.GetComponent<Pirate>().isStunned = true;
                stunSomeone = true;
            }
        }
        if (stunSomeone)
        {
            GameManager.currentEnergy--;
            character.GetComponent<Stats>().EmptyMeter();
            isInteractable = false;
        }
    }

    public void ActivateCleave(GameObject character)
    {
        bool hitsSomeone = false;
        for (int i = 0; i < CharacterManager.allEnemyCharacters.Count; i++)
        {
            GameObject enemy = CharacterManager.allEnemyCharacters[i];
            if (AdjacencyHandler.CompareAdjacency(character, enemy, 2))
            {
                enemy.GetComponent<Stats>().TakeDamage(3);
                enemy.GetComponent<Stats>().CheckDeath();
                hitsSomeone = true;
            }
        }
        if (hitsSomeone)
        {
            GameManager.currentEnergy--;
            character.GetComponent<Stats>().EmptyMeter();
            isInteractable = false;
        }
    }

    public void ActivateSprint(GameObject character, GameObject space)
    {
        int thisCharacterX = character.GetComponent<UnitCoordinates>().x;
        int thisCharacterY = character.GetComponent<UnitCoordinates>().y;

        int spaceX = space.GetComponent<GridCoordinates>().x;
        int spaceY = space.GetComponent<GridCoordinates>().y;

        int count = 0;

        for (int i = 0; i < CharacterManager.allCharacters.Count; i++)
        {
            int characterX = CharacterManager.allCharacters[i].GetComponent<UnitCoordinates>().x;
            int characterY = CharacterManager.allCharacters[i].GetComponent<UnitCoordinates>().y;

            if (spaceX != characterX || spaceY != characterY)
            {
                count++;
            }
        }

        if (count >= CharacterManager.allCharacters.Count)
        {
            Debug.Log("You are sprinting!");
            GameObject.Find("GridX" + thisCharacterX + "Y" + thisCharacterY).GetComponent<GridPiece>().unit = null;
            character.transform.position = space.transform.position;
            character.GetComponent<UnitCoordinates>().SetUnitCoordinates(spaceX, spaceY);
            space.GetComponent<GridPiece>().unit = character;
            character.GetComponent<Stats>().damage += 2;
            character.GetComponent<Stats>().SetCharging(true);
            GameManager.currentEnergy--;
            character.GetComponent<Stats>().EmptyMeter();
            isInteractable = false;
            inSelectionMode = false;
        }
    }

    public void ActivateFireball(GameObject character, GameObject enemy)
    {
        enemy.GetComponent<Stats>().TakeDamage(6);
        enemy.GetComponent<Stats>().CheckDeath();
        GameManager.currentEnergy--;
        character.GetComponent<Stats>().EmptyMeter();
        isInteractable = false;
        inSelectionMode = false;
    }

    public void TryFireball(GameObject character)
    {
        bool hasTarget = false;
        for (int i = 0; i < CharacterManager.allEnemyCharacters.Count; i++)
        {
            GameObject enemy = CharacterManager.allEnemyCharacters[i];
            if (AdjacencyHandler.CompareAdjacency(character, enemy, 2))
            {
                hasTarget = true;
            }
        }

        if (hasTarget)
        {
            inSelectionMode = true;
            currSelectionType = selectionType.enemy;
            currAbilityName = "fireball";
            RaycastManager.EmptyRaycastTargets();
        }
        else
        {
            Debug.Log("There is no legal target!");
        }
    }

    public void TrySprint(GameObject character)
    {
        inSelectionMode = true;
        currSelectionType = selectionType.emptySpace;
        currAbilityName = "sprint";
        RaycastManager.EmptyRaycastTargets();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isInteractable)
        {
            transform.GetComponent<Image>().color = Color.blue;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isInteractable)
        {
            transform.GetComponent<Image>().color = Color.white;
        }
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (isInteractable)
        {
            justClickedButton = true;
            ActivateAbility();
        }
    }
}
