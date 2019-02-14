using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Material highlightMat;
    [SerializeField] public Material normalMat;
    [SerializeField] private int totalEnergy;

    private int currentEnergy;
    private bool isPlayerTurn;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerTurn = true;

        if (totalEnergy == 0)
        {
            totalEnergy = 7;
        }

        currentEnergy = totalEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void refreshCurrentEnergy()
    {
        currentEnergy = totalEnergy;
    }

    public void reduceEnergy()
    {
        if (currentEnergy > 0)
        {
            currentEnergy--;
        }
        else
        {
            isPlayerTurn = false;
        }
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
