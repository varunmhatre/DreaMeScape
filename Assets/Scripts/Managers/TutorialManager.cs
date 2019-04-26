using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject tutorialGridManager;
    [SerializeField] GameObject cannonHandler;

    [SerializeField] GameObject indicatorParticles;
    [SerializeField] GameObject tutorialArrow;
    [SerializeField] GameObject surroundTiles;

    [SerializeField] List<GameObject> hudElements = new List<GameObject>();

    bool pirateTurn;
    bool piratesAdded;
    bool pirateTilesAdded;
    int enemyMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        pirateTurn = false;
        piratesAdded = false;
        pirateTilesAdded = false;
        GameManager.tutorialBlockClick = true;
        GameManager.tutorialBlockAbility = true;
        GameManager.currentEnergy = 5;
        GameManager.totalEnergy = 5;
        CharacterManager.allEnemyCharacters[0].GetComponent<Stats>().health += 6;
        enemyMaxHealth = CharacterManager.allEnemyCharacters[0].GetComponent<Stats>().health;

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
            BlockSpecificPlayersClick(new int[] { 0 });
            PauseDialog();
            indicatorParticles.transform.position = new Vector3(51.0f, 1.3f, 25.75f);
            indicatorParticles.SetActive(true);
            if (GetComponent<GridPieceSelect>().GetGridPieceCoords(6, 3).gameObject.GetComponent<GridPiece>().unit == null)
            {
                ResumeDialog();
                cannonHandler.transform.GetChild(0).gameObject.SetActive(false);
                indicatorParticles.SetActive(false);
                AllowSpecificPlayersClick(new int[] { 0 });
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
            if (!pirateTilesAdded)
            {
                Instantiate(surroundTiles, CharacterManager.allEnemyCharacters[0].transform);
                CharacterManager.allEnemyCharacters[0].transform.GetChild(4).gameObject.SetActive(false);
                CharacterManager.allEnemyCharacters[0].transform.GetChild(4).localPosition = new Vector3(-3.2f, 31.2f, -20.6f);
                CharacterManager.allEnemyCharacters[0].transform.GetChild(4).localScale = new Vector3(1.33f, 1.33f, 1.33f);
                pirateTilesAdded = true;
            }
        }

        if (DialoguePanelManager.stepIndex == 17)
        {
            BlockSpecificPlayersClick(new int[] { 0, 1, 2 });
            PauseDialog();
            indicatorParticles.transform.position = new Vector3(58.0f, 1.3f, 27.4f);
            indicatorParticles.SetActive(true);
            if(RaycastManager.rightClicked)
            {
                ResumeDialog();
                DialoguePanelManager.stepIndex++;
                DialoguePanelManager.countDialogueLength++;
                AllowSpecificPlayersClick(new int[] { 0, 1, 2 });
                indicatorParticles.SetActive(false);
            }
        }

        if (DialoguePanelManager.stepIndex == 20)
        {
            BlockSpecificPlayersClick(new int[] { 0, 1 });
            PauseDialog();
            CharacterManager.allEnemyCharacters[0].transform.GetChild(4).gameObject.SetActive(true);
            if(AdjacencyHandler.NumPlayerCharactersAround(CharacterManager.allEnemyCharacters[0], 1) == 1)
            {
                CharacterManager.allEnemyCharacters[0].transform.GetChild(4).gameObject.SetActive(false);
                ResumeDialog();
            }

        }

        if (DialoguePanelManager.stepIndex == 21)
        {
            PauseDialog();
            indicatorParticles.transform.position = new Vector3(58.0f, 1.3f, 27.4f);
            indicatorParticles.SetActive(true);
            if(CharacterManager.allEnemyCharacters[0].GetComponent<Stats>().health < enemyMaxHealth)
            {
                indicatorParticles.SetActive(false);
                ResumeDialog();
                AllowSpecificPlayersClick(new int[] { 0, 1 });
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
            CharacterManager.allEnemyCharacters[0].transform.GetChild(4).gameObject.SetActive(true);
            if (AdjacencyHandler.NumPlayerCharactersAround(CharacterManager.allEnemyCharacters[0], 1) == 3)
            {
                CharacterManager.allEnemyCharacters[0].transform.GetChild(4).gameObject.SetActive(false);
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
            Instantiate(surroundTiles, InteractablesManager.generators[0].transform);
            InteractablesManager.generators[0].transform.GetChild(4).localScale = new Vector3(1.33f, 1.33f, 1.33f);
        }

        if (DialoguePanelManager.stepIndex == 39)
        {
            InteractablesManager.generators[0].transform.GetChild(4).position = new Vector3(54.17f, 24.63f, 9f);
            InteractablesManager.generators[0].transform.GetChild(4).gameObject.SetActive(true);
            PauseDialog();
            if(InteractablesManager.generators[0].GetComponent<Generator>().isOn)
            {
                ResumeDialog();
            }
        }

        if (DialoguePanelManager.stepIndex == 40)
        {
            InteractablesManager.generators[0].transform.GetChild(4).gameObject.SetActive(false);
            //CharacterManager.allEnemyCharacters[0].SetActive(true);
            //CharacterManager.allEnemyCharacters[1].SetActive(true);
            if (!piratesAdded)
            {
                tutorialGridManager.GetComponent<TutorialBoardSetup>().AddMorePirates(2);
                piratesAdded = true;
            }
        }

        /*
        //Add positions for tut arrow in next many dialogs
        if (DialoguePanelManager.stepIndex == 48)
        {
            //tutorialArrow.transform.position = new Vector3(); //ed dream meter
            tutorialArrow.SetActive(true);
        }

        //Need to add dialog instructions on how to use abilities
        if (DialoguePanelManager.stepIndex == 49)
        {
            GameManager.tutorialBlockAbility = false;
            PauseDialog();
            //tutorialArrow.transform.position = new Vector3(); //ed ability icon
            if (CharacterManager.allAlliedCharacters[0].GetComponent<Stats>().meterUnitsFilled == 0)
            {
                ResumeDialog();
            }
            else
            {
                //CharacterManager.allAlliedCharacters[0].GetComponent<Stats>().GainMeter(5);
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
                //CharacterManager.allAlliedCharacters[2].GetComponent<Stats>().GainMeter(5);
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
                //CharacterManager.allAlliedCharacters[3].GetComponent<Stats>().GainMeter(5);
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
                //CharacterManager.allAlliedCharacters[1].GetComponent<Stats>().GainMeter(5);
            }
        }
        */
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

    private void BlockSpecificPlayersClick(int[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            CharacterManager.allAlliedCharacters[players[i]].tag = "Untagged";
        }
    }
    private void AllowSpecificPlayersClick(int[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            CharacterManager.allAlliedCharacters[players[i]].tag = "Player";
        }
    }
}
