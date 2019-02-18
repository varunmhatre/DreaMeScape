using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCrew : MonoBehaviour
{
    private int x;
    private int y;
    private int numCharsToSurround;

    private bool hasBeenSurrounded;

    void Start()
    {
        numCharsToSurround = 3;
        hasBeenSurrounded = false;
    }

    void Update()
    {
        gameObject.GetComponent<Stats>().CheckDeath();
        gameObject.GetComponent<Stats>().UpdateDisplay();
        CheckCharactersSurround();
    }

    void SetCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void CheckCharactersSurround()
    {       
        if(!hasBeenSurrounded)
        {
            if (AdjacencyHandler.NumPlayerCharactersAround(gameObject, 1) >= numCharsToSurround)
            {
                if (gameObject.GetComponent<StatsTextDisplay>().GetInitialHealth() % 2 == 1)
                {
                    gameObject.GetComponent<Stats>().health -= (gameObject.GetComponent<StatsTextDisplay>().GetInitialHealth()/2 + 1);
                }
                else
                {
                    gameObject.GetComponent<Stats>().health -= gameObject.GetComponent<StatsTextDisplay>().GetInitialHealth()/2;
                }
                if (gameObject.GetComponent<StatsTextDisplay>().GetInitialAttack() % 2 == 1)
                {
                    gameObject.GetComponent<Stats>().damage -= (gameObject.GetComponent<StatsTextDisplay>().GetInitialAttack() / 2 + 1);
                }
                else
                {
                    gameObject.GetComponent<Stats>().damage -= gameObject.GetComponent<StatsTextDisplay>().GetInitialAttack() / 2;
                }
                hasBeenSurrounded = true;
            }
        }
    }
}
