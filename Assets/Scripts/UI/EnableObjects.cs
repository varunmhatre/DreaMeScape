using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] size;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(DialoguePanelManager.playerControlsUnlocked && !TutorialCards.isTutorialRunning)
        {
            for(int i = 0; i < size.Length; i++)
            {
                if (size != null)
                {
                    size[i].SetActive(true);
                }                    
            }            
        }
        else
        {
            for (int i = 0; i < size.Length; i++)
            {
                if (size != null)
                {
                    size[i].SetActive(false);
                }
            }
        }
    }
}
