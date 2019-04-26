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
    public static int stepIndex = -1;
    public static bool isPressed;
    public static bool isPaused;
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

        Debug.Log("here");
    }
    public void BootSequence()
    {        
        if (GameObject.Find("CharacterPanel") != null)
        { 
            characterPanel = GameObject.Find("CharacterPanel").GetComponent<DialoguePanelConfig>();
        }
        
        if (SceneManager.GetActiveScene().name == "TutorialScene")
        {
            currentEvent = JSONAssembly.RunJSONFactoryForScene(1); 
        }
        if (SceneManager.GetActiveScene().name == "FantasyWorldStartScene")
        {
            currentEvent = JSONAssembly.RunJSONFactoryForScene(3);
        }
        if (SceneManager.GetActiveScene().name == "PirateshipScene")
        { 
            currentEvent = JSONAssembly.RunJSONFactoryForScene(2);
        }
        InitiziliasePanels();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPressed == true && !isPaused)
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
            if (SceneManager.GetActiveScene().name == "TutorialScene")
            {
                stepIndex = -1;
                countDialogueLength = 0;
                //Uncomment when tut ready
                SceneManager.LoadScene("PirateshipScene");
            }

            if (SceneManager.GetActiveScene().name == "PirateshipScene")
            { 
                dialoguePanel.SetActive(false);
                playerControlsUnlocked = true;
                isCharacterPanelDisabled = true;
                countDialogueLength =  currentEvent.dialogues.Count;
            }
        }       
        else if (countDialogueLength < currentEvent.dialogues.Count && !isPaused)
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
    public void UpdatePanelState()
    {
        if(stepIndex < currentEvent.dialogues.Count)
        {
            ConfigurePanels();

            CharacterActive = !CharacterActive;

            stepIndex++;             
        }
    }
}
