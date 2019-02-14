using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDTextScript : MonoBehaviour
{
    [SerializeField] Text[] presenceTextArr;
    [SerializeField] Text[] resistTextArr;
    [SerializeField]
    private GameObject[] characterPortrait;
    private int healthValue;
    private int attackValue;    
    public static bool isVisible;

    // Use this for initialization
    void Start ()
    {
       
    }
	
	// Update is called once per frame
	void Update ()
    {       
        UpdateText();        
    }

    void UpdateText()
    {    
        if(!DialoguePanelManager.playerControlsLocked && !TutorialCards.isTutorialRunning)
        {
            for(int i = 0; i < characterPortrait.Length; i++)
            {
                characterPortrait[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < characterPortrait.Length; i++)
            {
                characterPortrait[i].SetActive(false);
            }
        }
    }
}
