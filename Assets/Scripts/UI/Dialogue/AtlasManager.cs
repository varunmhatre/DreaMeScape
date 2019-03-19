using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AtlasManager : MonoBehaviour, DialogueStateManager
{
    public static Sprite[] sprites;
    public static Sprite[] dialogueSprites;
    public static Sprite[] nameSprites;
    public ManagerState currentState
    {
        get;
        private set;
    }

    public void BootSequence()
    {
        sprites = Resources.LoadAll<Sprite>("CharPortrait");
        dialogueSprites = Resources.LoadAll<Sprite>("DialogueTextBox");
        nameSprites = Resources.LoadAll<Sprite>("Namebox");
        currentState = ManagerState.completed;         
    }

    public Sprite loadSprite(string spriteName)
    {
        foreach (Sprite S in sprites)
        {
            if(S.name == spriteName)
            {
                return S;
            }
        }
        return null;
    }	
    public Sprite loadTextbox(string textboxName)
    {
        foreach (Sprite S in dialogueSprites)
        {
            if (S.name == textboxName)
            {
               
                return S;
            }
        }
        return null;
    }
    public Sprite loadNamebox(string textboxName)
    {
        foreach (Sprite S in nameSprites)
        {
            if (S.name == textboxName)
            {

                return S;
            }
        }
        return null;
    }
}
