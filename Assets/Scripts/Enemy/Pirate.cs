using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : MonoBehaviour
{
    [SerializeField]
    public int noOfTurns
    {
        get; private set;
    }

    public int moveCounter;

    void Start()
    {
        moveCounter = 0;
    }
}
