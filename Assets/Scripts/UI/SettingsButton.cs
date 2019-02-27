using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject controlDisplay;
    public static bool isClicked;
    // Use this for initialization
    void Start()
    {
        isClicked = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ControlAction()
    {
        isClicked = !isClicked; 
        if (isClicked)
        {
            controlDisplay.SetActive(true);
        }
        else 
        {
            controlDisplay.SetActive(false);
        }
    }
}
