using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlaygroundController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   // [SerializeField]
   // private Button[] buttonSize;
    public static bool isMouseover;
    public Sprite onSprite; 
    public Sprite offSprite;

    [SerializeField]
    private string buttonText;
    //public Transform target;
    //public Transform to;

    private float timeCount = 0.0f;
    private float flipSpeed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        isMouseover = false;        
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
        transform.GetComponent<Image>().sprite = onSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetComponent<Image>().sprite = offSprite; 
    }      
}
