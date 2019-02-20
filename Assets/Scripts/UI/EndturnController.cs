using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndturnController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public static bool isInteractable;

    // Start is called before the first frame update
    void Start()
    {
        isInteractable = false;
    }
    void Update()
    {
        if(!GameManager.HaveEnergy())
        {
            transform.GetComponent<Image>().enabled = true;
        }
        if(!TutorialCards.isTutorialRunning)
        {
            isInteractable = true;
        }

        CheckEndTurn();
    } 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isInteractable)
        {
            transform.GetComponent<Image>().enabled = true;
        }        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isInteractable)
        {
            transform.GetComponent<Image>().enabled = false;
        }                 
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if(isInteractable)
        {
            GameManager.RefreshCurrentEnergy();
            GameManager.RefreshCharacters();
            transform.GetComponent<Image>().enabled = false;
            isInteractable = false;
        }
    }

    public void CheckEndTurn()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.RefreshCurrentEnergy();
            GameManager.RefreshCharacters();
            transform.GetComponent<Image>().enabled = false;
            isInteractable = false;
        }
    }
}
