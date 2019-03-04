using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class testScript : MonoBehaviour
{ 
    public EventSystem eventSystem; //select event system in editor
    private GameObject lastSelectedButton;
    private GameObject currentSelectedButton;
    private GameObject currentSelectedGameObject_Recent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
     
    public void CharacterSelect()
    {
        GetLastGameObjectSelected();
    }
    private void GetLastGameObjectSelected()
    {
        if (eventSystem.currentSelectedGameObject != currentSelectedGameObject_Recent)
        {
            lastSelectedButton = currentSelectedGameObject_Recent;
            currentSelectedGameObject_Recent = eventSystem.currentSelectedGameObject;
            Debug.Log("lastSelectedGameObject:  " + lastSelectedButton);
            Debug.Log("currentSelectedGameObject_Recent:  " + currentSelectedGameObject_Recent); 
        }
    }
}
