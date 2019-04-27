using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfNeedsToBeEnabled : MonoBehaviour
{
    [SerializeField] int characterId;
    [SerializeField] GameObject greyedOutImage;
    public bool isEnabled;

    private void Start()
    {
        greyedOutImage.SetActive(false);
        isEnabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPlayerTurn)
        {
            if (!isEnabled && CharacterManager.allAlliedCharacters[characterId])
            {
                if (CharacterManager.allAlliedCharacters[characterId].GetComponent<Stats>().hasAttacked)
                {
                    isEnabled = true;
                    greyedOutImage.SetActive(true);
                }
            }
        }
        else
        {
            if (CharacterManager.allAlliedCharacters[characterId])
            {
                isEnabled = false;
                greyedOutImage.SetActive(false);
            }
        }
    }
}
