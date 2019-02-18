using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject controlDisplay;
    private bool isClicked;
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
