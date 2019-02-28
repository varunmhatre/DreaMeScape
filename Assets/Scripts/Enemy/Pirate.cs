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

    public void GetStunned()
    {
        isStunned = true;
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update()
    {
        gameObject.GetComponent<Stats>().UpdateDisplay();
    }
}