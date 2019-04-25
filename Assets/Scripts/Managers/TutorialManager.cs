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
    bool pirateTurn;

    // Start is called before the first frame update
    void Start()
    {
        pirateTurn = false;
        GameManager.tutorialBlockClick = true;
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
                ResumeDialog();
                cannonHandler.transform.GetChild(0).gameObject.SetActive(false);
                indicatorParticles.SetActive(false);
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
            indicatorParticles.transform.position = new Vector3(58.0f, 1.3f, 27.4f);
            indicatorParticles.SetActive(true);
        }

        if (DialoguePanelManager.stepIndex == 18)
        {
            indicatorParticles.SetActive(false);
        }

        if (DialoguePanelManager.stepIndex == 20)
        {
            PauseDialog();
            surroundTiles.transform.position = new Vector3(55.6f, 24.63f, 11.9f);
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
            indicatorParticles.transform.position = new Vector3(58.0f, 1.3f, 27.4f);
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
            if (GameManager.currentEnergy <= 0)
            {
                ResumeDialog();
            }
        }

        if (DialoguePanelManager.stepIndex == 30)
        {
            pirateTurn = true;
        }

        if (DialoguePanelManager.stepIndex == 31)
        {
            tutorialArrow.SetActive(true);
            PauseDialog();
            if (pirateTurn)
            {
                GameManager.EndCurrentTurn();
                pirateTurn = false;
            }

            if (GameManager.isPlayerTurn)
            {
                ResumeDialog();
            }
        }


        //End of pirates turn

        if (DialoguePanelManager.stepIndex == 32)
        {
            tutorialArrow.SetActive(false);
            //GameManager.RefreshCurrentEnergy();
        }

        if (DialoguePanelManager.stepIndex == 34)
        {
            PauseDialog();
            //surroundTiles.transform.position = new Vector3(55.6f, 24.63f, 11.9f);
            //surroundTiles.SetActive(true);
            if (AdjacencyHandler.NumPlayerCharactersAround(CharacterManager.allEnemyCharacters[0], 1) == 3)
            {
                surroundTiles.SetActive(false);
                ResumeDialog();
            }
        }
        
        if (DialoguePanelManager.stepIndex == 35)
        {
            PauseDialog();
            if(CharacterManager.allEnemyCharacters.Count == 0)
            {
                ResumeDialog();
            }
        }

        if (DialoguePanelManager.stepIndex == 37)
        {
            InteractablesManager.generators[0].SetActive(true);
        }

        if (DialoguePanelManager.stepIndex == 39)
        {
            surroundTiles.transform.position = new Vector3(54.17f, 24.63f, 9f);
            surroundTiles.SetActive(true);
            PauseDialog();
            if(InteractablesManager.generators[0].GetComponent<Generator>().isOn)
            {
                ResumeDialog();
            }
        }

        if (DialoguePanelManager.stepIndex == 40)
        {
            surroundTiles.SetActive(false);
            //CharacterManager.allEnemyCharacters[0].SetActive(true);
            //CharacterManager.allEnemyCharacters[1].SetActive(true);
        }

        //Add positions for tut arrow in next many dialogs
        if (DialoguePanelManager.stepIndex == 48)
        {
            //tutorialArrow.transform.position = new Vector3(); //ed dream meter
            tutorialArrow.SetActive(true);
        }

        //Need to add dialog instructions on how to use abilities
        if (DialoguePanelManager.stepIndex == 49)
        {
            PauseDialog();
            //tutorialArrow.transform.position = new Vector3(); //ed ability icon
            if (CharacterManager.allAlliedCharacters[0].GetComponent<Stats>().meterUnitsFilled == 0)
            {
                ResumeDialog();
            }
            else
            {
                CharacterManager.allAlliedCharacters[0].GetComponent<Stats>().GainMeter(5);
            }
        }

        if (DialoguePanelManager.stepIndex == 52)
        {
            PauseDialog();
            //tutorialArrow.transform.position = new Vector3(); //jade ability icon
            if (CharacterManager.allAlliedCharacters[2].GetComponent<Stats>().meterUnitsFilled == 0)
            {
                ResumeDialog();
            }
            else
            {
                CharacterManager.allAlliedCharacters[2].GetComponent<Stats>().GainMeter(5);
            }
        }

        if (DialoguePanelManager.stepIndex == 54)
        {
            PauseDialog();
            //tutorialArrow.transform.position = new Vector3(); //meda ability icon
            if (CharacterManager.allAlliedCharacters[3].GetComponent<Stats>().meterUnitsFilled == 0)
            {
                ResumeDialog();
            }
            else
            {
                CharacterManager.allAlliedCharacters[3].GetComponent<Stats>().GainMeter(5);
            }
        }

        if (DialoguePanelManager.stepIndex == 56)
        {

            PauseDialog();
            //tutorialArrow.transform.position = new Vector3(); //hally ability icon
            if (CharacterManager.allAlliedCharacters[1].GetComponent<Stats>().meterUnitsFilled == 0)
            {
                ResumeDialog();
            }
            else
            {
                CharacterManager.allAlliedCharacters[1].GetComponent<Stats>().GainMeter(5);
            }
        }

        if (DialoguePanelManager.stepIndex == 52)
        {

        }

        if (DialoguePanelManager.stepIndex == 52)
        {

        }

    }

    private void ShowCharacter(int charNum)
    {
        CharacterManager.allAlliedCharacters[charNum].SetActive(true);
        hudElements[charNum].SetActive(true);
    }

    private void PauseDialog()
    {
        GameManager.tutorialBlockClick = false;

        DialoguePanelManager.isPaused = true;
        DialoguePanelManager.playerControlsUnlocked = true;
        
        if (GameManager.currentEnergy <= 0 && DialoguePanelManager.stepIndex != 28)
        {
            GameManager.RefreshCurrentEnergy();
        }
    }
    private void ResumeDialog()
    {
        GameManager.tutorialBlockClick = true;

        DialoguePanelManager.playerControlsUnlocked = false;
        DialoguePanelManager.isPaused = false;
    }
}
