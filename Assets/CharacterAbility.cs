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

    // Start is called before the first frame update
    void Start()
    {
        amountMeterNeeded = 5;
        isInteractable = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfMeterFull(buttonId, CharacterManager.allAlliedCharacters[buttonId].GetComponent<Stats>()))
        {            
            isInteractable = true;
        }
        
        gameObject.GetComponent<Button>().interactable = isInteractable;
    }

    public void ActivateAbility()
    {
        transform.GetComponent<Image>().color = Color.blue;
        Stats charStats = CharacterManager.allAlliedCharacters[buttonId].GetComponent<Stats>();
        if (buttonId == 3 && GameManager.currentEnergy >= 1)
        {
            ActivateCleave(charStats.gameObject);
        }
    }

    public bool CheckIfMeterFull(int id, Stats charStats)
    {
       // Debug.Log(charStats);
        if (buttonId == id && charStats.meterUnitsFilled >= amountMeterNeeded)
        {
            return true;
        }

        return false;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isInteractable)
        {
            Debug.Log("OnPointerEnter");
            transform.GetComponent<Image>().color = Color.blue;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isInteractable)
        {
            Debug.Log("OnPointerExit");
            transform.GetComponent<Image>().color = Color.white;
        }
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (isInteractable)
        {
            ActivateAbility();
        }
    }
}
