using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DialoguePanelManager.stepIndex == 2)
        {
            PauseDialog();
            if(GetComponent<GridPieceSelect>().GetGridPieceCoords(4, 5).gameObject.GetComponent<GridPiece>().unit == CharacterManager.allAlliedCharacters[1])
            {
                ResumeDialog();
            }
        }

        if(DialoguePanelManager.stepIndex == 5)
        {
            CharacterManager.allAlliedCharacters[4].SetActive(true);
            InteractablesManager.generators[0].SetActive(true);
        }
    }

    private void PauseDialog()
    {
        DialoguePanelManager.isPaused = true;
        DialoguePanelManager.playerControlsUnlocked = true;
    }
    private void ResumeDialog()
    {
        DialoguePanelManager.playerControlsUnlocked = false;
        GetComponent<GameManager>().dialogSceneController.GetComponent<DialoguePanelManager>().BootSequence();
        DialoguePanelManager.isPaused = false;
    }
}
