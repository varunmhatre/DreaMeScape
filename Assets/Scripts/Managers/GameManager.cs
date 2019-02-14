using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Material highlightMat;
    [SerializeField] public Material normalMat;
    [SerializeField] private int totalEnergy;

    private int currentEnergy;
    public bool isPlayerTurn;

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

    public void RefreshCurrentEnergy()
    {
        currentEnergy = totalEnergy;
    }

    public void ReduceEnergy()
    {
        if (currentEnergy > 0)
        {
            currentEnergy--;
        }
    }

    public bool HaveEnergy()
    {
        return currentEnergy > 0;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
