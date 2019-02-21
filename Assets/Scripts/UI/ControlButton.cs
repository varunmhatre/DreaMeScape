using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject controlDisplay;
    private bool isClicked;
    // Use this for initialization
    void Start ()
    {
        isClicked = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
               
    }
     
    public void ControlAction()
    {
        //if(!isClicked)
        //{            
        //    isClicked = true;
        //    controlDisplay.SetActive(true);
        //}
        //else if(isClicked == true)
        //{
        //    isClicked = false;
        //    controlDisplay.SetActive(false);            
        //}
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (!isClicked)
        {
            isClicked = true;
            controlDisplay.SetActive(true);
        }
        else if (isClicked == true)
        {
            isClicked = false;
            controlDisplay.SetActive(false);
        }
    }
}
