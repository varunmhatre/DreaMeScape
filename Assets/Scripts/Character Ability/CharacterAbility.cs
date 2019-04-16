using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

struct ColorRendererCombo
{
    public Renderer renderer;
    public Color color;
    public Material material;
    public ColorRendererCombo(Renderer ren)
    {
        renderer = ren;
        material = renderer.material;
        color = material.color;
    }
}

public class CharacterAbility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] GameObject tooltipObj;
    private int amountMeterNeeded;
    [SerializeField] private int buttonId;
    private static int currButtonId;
    private bool isInteractable;
    private string currAbilityName;
    public static bool inSelectionMode;

    public enum selectionType
    {
        ally,
        enemy,
        emptySpace
    }

    [SerializeField] private selectionType currSelectionType;

    // Start is called before the first frame update
    void Start()
    {
        currButtonId = -1;
        amountMeterNeeded = 5;
        isInteractable = false;
        inSelectionMode = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (buttonId >= CharacterManager.allAlliedCharacters.Count || CharacterManager.allAlliedCharacters[buttonId] == null)
        {
            return;
        }
        if (CheckIfMeterFull(buttonId, CharacterManager.allAlliedCharacters[buttonId].GetComponent<Stats>()))
        {            
            isInteractable = true;
        }

        gameObject.GetComponent<Button>().interactable = isInteractable;

        if (inSelectionMode && currButtonId == buttonId)
        {
            CheckSelection(currSelectionType, currAbilityName);
        }
    }

    public void ActivateAbility()
    {
        Stats charStats = CharacterManager.allAlliedCharacters[buttonId].GetComponent<Stats>();
        if (buttonId == 3 && GameManager.currentEnergy >= 1)
        {
            transform.GetComponent<Image>().color = new Color(255.0f, 165.0f, 0.0f);
            TryFireball(charStats.gameObject);
        }        
        else if (buttonId == 4 && GameManager.currentEnergy >= 1)
        {
            transform.GetComponent<Image>().color = Color.blue;
            ActivateCleave(charStats.gameObject);
        }
        else if (buttonId == 2 && GameManager.currentEnergy >= 1)
        {
            transform.GetComponent<Image>().color = new Color(10.0f, 10.0f, 10.0f);
            TrySprint(charStats.gameObject);
        }
        else if (buttonId == 1 && GameManager.currentEnergy >= 1)
        {
            transform.GetComponent<Image>().color = new Color(10.0f, 10.0f, 10.0f);
            ActivateBolster(charStats.gameObject);
        }
        else if (buttonId == 0 && GameManager.currentEnergy >= 1)
        {
            transform.GetComponent<Image>().color = Color.yellow;
            ActivateParalyzingPotion(charStats.gameObject);
        }
        else
        {
            transform.GetComponent<Image>().color = new Color(235.0f, 235.0f, 30.0f);
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
                if (RaycastManager.leftClicked)
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
                if (RaycastManager.leftClicked)
                {
                    RaycastHit hitPiece = RaycastManager.GetRaycastHitForTag("GridPiece");
                    if (hitPiece.transform != null)
                    {
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

    //Henry's Ability
    public void ActivateBolster(GameObject character)
    {
        bool buffSomeone = false;
        GetComponent<HallyAbilityHandler>().OnMouseClickWhenOn();
        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            GameObject ally = CharacterManager.allAlliedCharacters[i];
            if (AdjacencyHandler.CompareAdjacency(character, ally, 2))
            {
                ally.GetComponent<Stats>().GainMeter(3);
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
        bool hitsSomeone = false;
        GetComponent<EdAbilityHandler>().OnMouseClickWhenOn();
        for (int i = 0; i < CharacterManager.allEnemyCharacters.Count; i++)
        {
            GameObject enemy = CharacterManager.allEnemyCharacters[i];
            if (AdjacencyHandler.CompareAdjacency(character, enemy, 2))
            {
                enemy.GetComponent<Pirate>().GetStunned();
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

    public void ActivateCleave(GameObject character)
    {
        bool hitsSomeone = false;
        GetComponent<KentAbilityHandler>().OnMouseClickWhenOn();
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
        UnitCoordinates characterCoord = character.GetComponent<UnitCoordinates>();
        int thisCharacterX = characterCoord.x;
        int thisCharacterY = characterCoord.y;

        GridCoordinates gridCoord = space.GetComponent<GridCoordinates>();
        int spaceX = gridCoord.x;
        int spaceY = gridCoord.y;
        
        if (!GameObject.Find("GridX" + thisCharacterX + "Y" + thisCharacterY))
            return;

        GridPiece grid = GameObject.Find("GridX" + thisCharacterX + "Y" + thisCharacterY).GetComponent<GridPiece>();
        if (space.GetComponent<GridPiece>().unit != null)
            return;

        grid.unit = null;
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
            GetComponent<MedaAbilityHandler>().OnMouseClickWhenOn();
            inSelectionMode = true;
            currAbilityName = "fireball";
            RaycastManager.EmptyRaycastTargets();
        }
    }

    public void TrySprint(GameObject character)
    {
        inSelectionMode = true;
        //currSelectionType = selectionType.emptySpace;
        currAbilityName = "sprint";
        RaycastManager.EmptyRaycastTargets();
        GetComponent<JadeAbilityHandler>().OnMouseClickWhenOn();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TutorialCards.isTutorialRunning)
        {
            transform.GetComponent<Image>().color = Color.blue;
            if (buttonId == 0)
            {
                if (tooltipObj != null)
                {
                    tooltipObj.SetActive(true);
                }
                GetComponent<EdAbilityHandler>().OnMouseHoveringStart();
            }
            else if (buttonId == 1)
            {
                if (tooltipObj != null)
                {
                    tooltipObj.SetActive(true);
                }
                GetComponent<HallyAbilityHandler>().OnMouseHoveringStart();
            }
            else if (buttonId == 2)
            {
                if (tooltipObj != null)
                {
                    tooltipObj.SetActive(true);
                }
                GetComponent<JadeAbilityHandler>().OnMouseHoveringStart();
            }
            else if (buttonId == 4)
            {
                if (tooltipObj != null)
                {
                    tooltipObj.SetActive(true);
                }
                GetComponent<KentAbilityHandler>().OnMouseHoveringStart();
            }
            else if (buttonId == 3)
            {
                if (tooltipObj != null)
                {
                    tooltipObj.SetActive(true);
                }
                GetComponent<MedaAbilityHandler>().OnMouseHoveringStart();
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (TutorialCards.isTutorialRunning)
        {
            transform.GetComponent<Image>().color = Color.white;
            if (buttonId == 0)
            {
                GetComponent<EdAbilityHandler>().OnMouseHoveringExit();
            }
            else if (buttonId == 1)
            {
                GetComponent<HallyAbilityHandler>().OnMouseHoveringExit();
            }
            else if (buttonId == 2)
            {
                GetComponent<JadeAbilityHandler>().OnMouseHoveringExit();
            }
            else if (buttonId == 4)
            {
                GetComponent<KentAbilityHandler>().OnMouseHoveringExit();
            }
            else if (buttonId == 3)
            {
                GetComponent<MedaAbilityHandler>().OnMouseHoveringExit();
            }
        }

        if (tooltipObj != null)
        {
            tooltipObj.SetActive(false);
        }
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (isInteractable)
        {
            currButtonId = buttonId;
            ActivateAbility();
        }
    } 
}
