using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject indicatorParticles;
    [SerializeField] GameObject cannonHandler;
    [SerializeField] GameObject tutorialArrow;
    [SerializeField] GameObject surroundTiles;
    [SerializeField] List<GameObject> hudElements = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (DialoguePanelManager.stepIndex == 1)
        {
            ShowCharacter(1);
        }

        if (DialoguePanelManager.stepIndex == 6)
        {
            cannonHandler.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (DialoguePanelManager.stepIndex == 7)
        {
            PauseDialog();
            indicatorParticles.transform.position = new Vector3(51.0f, 1.3f, 25.75f);
            indicatorParticles.SetActive(true);
            if (GetComponent<GridPieceSelect>().GetGridPieceCoords(6, 3).gameObject.GetComponent<GridPiece>().unit == null)
            {
                indicatorParticles.SetActive(false);
                ResumeDialog();
            }
        }

        if (DialoguePanelManager.stepIndex == 8)
        {
            tutorialArrow.SetActive(true);
        }

        if (DialoguePanelManager.stepIndex == 9)
        {
            tutorialArrow.SetActive(false);
        }

        if (DialoguePanelManager.stepIndex == 12)
        {
            ShowCharacter(2);
        }

        if (DialoguePanelManager.stepIndex == 13)
        {
            CharacterManager.allEnemyCharacters[0].SetActive(true);
        }

        if (DialoguePanelManager.stepIndex == 17)
        {
            indicatorParticles.transform.position = new Vector3(55.0f, 1.5f, 28.75f);
            indicatorParticles.SetActive(true);
        }

        if (DialoguePanelManager.stepIndex == 18)
        {
            indicatorParticles.SetActive(false);
        }

        if (DialoguePanelManager.stepIndex == 20)
        {
            PauseDialog();
            surroundTiles.transform.position = new Vector3(52.75f, 24.63f, 13.3f);
            surroundTiles.SetActive(true);
            if(AdjacencyHandler.NumPlayerCharactersAround(CharacterManager.allEnemyCharacters[0], 1) == 1)
            {
                surroundTiles.SetActive(false);
                ResumeDialog();
            }
        }

        if (DialoguePanelManager.stepIndex == 21)
        {
            PauseDialog();
            indicatorParticles.transform.position = new Vector3(55.0f, 1.5f, 28.75f);
            indicatorParticles.SetActive(true);
            if(CharacterManager.allEnemyCharacters[0].GetComponent<Stats>().health < 6)
            {
                indicatorParticles.SetActive(false);
                ResumeDialog();
            }
        }

        if (DialoguePanelManager.stepIndex == 26)
        {
            ShowCharacter(3);
        }

        if (DialoguePanelManager.stepIndex == 28)
        {
            PauseDialog();
            if(GameManager.currentEnergy <= 0)
            {
                ResumeDialog();
            }
        }

        if (DialoguePanelManager.stepIndex == 39)
        {
            GameManager.RefreshCurrentEnergy();
            surroundTiles.transform.position = new Vector3(54.17f, 24.63f, 9f);
            surroundTiles.SetActive(true);
            PauseDialog();
            if(InteractablesManager.generators[0].GetComponent<Generator>().isOn)
            {
                ResumeDialog();
            }
        }

        if (DialoguePanelManager.stepIndex == 5)
        {

        }

        //if (DialoguePanelManager.stepIndex == -1)
        //{
        //    ShowCharacter(3);
        //    InteractablesManager.generators[0].SetActive(true);
        //}
    }

    private void ShowCharacter(int charNum)
    {
        CharacterManager.allAlliedCharacters[charNum].SetActive(true);
        hudElements[charNum].SetActive(true);
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
