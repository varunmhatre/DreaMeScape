using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : MonoBehaviour
{
    [SerializeField] int noOfTurns;

    public int GetNumberOfTurns()
    {
        return noOfTurns;
    }

    public bool isStunned;

    void Update()
    {
        gameObject.GetComponent<Stats>().UpdateDisplay();
    }
}