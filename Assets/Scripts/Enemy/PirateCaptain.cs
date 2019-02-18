using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCaptain : MonoBehaviour
{
    int x;
    int y;

    void Start()
    {

    }

    void Update()
    {
        gameObject.GetComponent<Stats>().CheckDeath();
        gameObject.GetComponent<Stats>().UpdateDisplay();
    }

    void SetCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
}
