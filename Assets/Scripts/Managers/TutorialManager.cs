using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject SurroundTiles;
    [SerializeField] List<GameObject> HUDElements = new List<GameObject>();

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
            if(GetComponent<GridPieceSelect>().GetGridPieceCoords(6, 4).gameObject.GetComponent<GridPiece>().unit == null)
            {
                ResumeDialog();
            }
        }

        if (DialoguePanelManager.stepIndex == 5)
        {
            CharacterManager.allAlliedCharacters[3].SetActive(true);
            HUDElements[3].SetActive(true);
            InteractablesManager.generators[0].SetActive(true);
        }

        if (DialoguePanelManager.stepIndex == 10)
        {
            GameManager.RefreshCurrentEnergy();
            SurroundTiles.SetActive(true);
            PauseDialog();
            if(InteractablesManager.generators[0].GetComponent<Generator>().isOn)
            {
                ResumeDialog();
            }
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
