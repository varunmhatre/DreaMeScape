using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyEndAnimation : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.HaveEnergy() || !GameManager.isPlayerTurn)
        {
            gameObject.GetComponent<Image>().enabled = false;
        }
        else 
        {
            gameObject.GetComponent<Image>().enabled = true;
        }
    }
}
