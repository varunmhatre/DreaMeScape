using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndturnController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool isInteractable;

    // Start is called before the first frame update
    void Start()
    {
        isInteractable = false;
    }
    void Update()
    {
        
    } 
    public void OnClick()
    {
        isInteractable = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetComponent<Image>().enabled = true;     
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isInteractable)
        {
            transform.GetComponent<Image>().enabled = false;
        }                 
    }
}
