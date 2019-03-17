using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsButton : MonoBehaviour
{
    public GameObject controlDisplay;
    public static bool isClicked;
    // Use this for initialization
    void Start()
    {
        isClicked = false;
        transform.GetComponent<Button>().interactable = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(TutorialCards.isTutorialRunning)
        {
            transform.GetComponent<Button>().interactable = true;
        }
    }
    public void ControlAction()
    {
        if(TutorialCards.isTutorialRunning)
        {
            isClicked = !isClicked;
            if (isClicked)
            {
                controlDisplay.SetActive(true); 
                transform.GetComponentInChildren<Text>().text = "X";
            }
            else
            {
                SettingsHandler.currentIndex = 0;
                transform.GetComponentInChildren<Text>().text = "?";
                controlDisplay.SetActive(false);
            }
        }        
    }
}
