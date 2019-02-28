using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPortrait : MonoBehaviour
{
    [SerializeField]
    private GameObject[] characterPortrait;

    private void Update()
    {
        DisplayCharacterPortrait();
    }
    void DisplayCharacterPortrait()
    {
        if (DialoguePanelManager.playerControlsUnlocked && !TutorialCards.isTutorialRunning)
        {
            for (int i = 0; i < characterPortrait.Length; i++)
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
