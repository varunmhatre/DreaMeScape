using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONText;
using UnityEngine.SceneManagement;

public class DialoguePanelManager : MonoBehaviour, DialogueStateManager
{
    public ManagerState currentState { get; private set; }
    private DialoguePanelConfig characterPanel;
    private NarrativeEvent currentEvent;
    private bool CharacterActive = true;
    private int stepIndex = -1;
    public static bool isPressed;
    public static bool playerControlsUnlocked;
    public static int countDialogueLength;
    [SerializeField] private int maxCountDialogueLength;

    [SerializeField]
    private GameObject dialoguePanel;

    private bool isCharacterPanelDisabled;

    void Start()
    {
        playerControlsUnlocked = false;
        isCharacterPanelDisabled = false;
    }
    public void BootSequence()
    { 
        if(GameObject.Find("CharacterPanel") != null)
        {
            characterPanel = GameObject.Find("CharacterPanel").GetComponent<DialoguePanelConfig>();
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            currentEvent = JSONAssembly.RunJSONFactoryForScene(1); 
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            currentEvent = JSONAssembly.RunJSONFactoryForScene(1);
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            currentEvent = JSONAssembly.RunJSONFactoryForScene(2);
        }
        InitiziliasePanels();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPressed == true)
        {
            isPressed = false; 
            if(DialoguePanelConfig.isDialogueTextOver)
            {               
                UpdatePanelState();                
            }
            BootSequence();
        } 
        if (Input.GetKey(KeyCode.P) || countDialogueLength >= currentEvent.dialogues.Count)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                dialoguePanel.SetActive(false);
                countDialogueLength = 0;
                stepIndex = 0;
                playerControlsUnlocked = true;
                SceneManager.LoadScene("TutorialScene");
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                dialoguePanel.SetActive(false);
                countDialogueLength = 0;
                stepIndex = 0;
                playerControlsUnlocked = true;
                SceneManager.LoadScene("PirateshipScene");
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                
                dialoguePanel.SetActive(false);
                playerControlsUnlocked = true;
                isCharacterPanelDisabled = true;
                countDialogueLength = currentEvent.dialogues.Count;
            }
        }       
        else if (countDialogueLength < currentEvent.dialogues.Count)
        {
            characterPanel.isTalking = false;           
            playerControlsUnlocked = false;
        }
    }
    private void InitiziliasePanels()
    { 
        if(!isCharacterPanelDisabled)
        {
            characterPanel.isTalking = true;
            stepIndex++;
            countDialogueLength++;
            characterPanel.Configure(currentEvent.dialogues[stepIndex]);
            CharacterActive = !CharacterActive;
        }       
    }
    private void ConfigurePanels()
    {
        if(CharacterActive)
        {
            characterPanel.Configure(currentEvent.dialogues[stepIndex]);
        }
    }
    void UpdatePanelState()
    {
        if(stepIndex < currentEvent.dialogues.Count)
        {
            ConfigurePanels();

            CharacterActive = !CharacterActive;

            stepIndex++;             
        }
    }
}
