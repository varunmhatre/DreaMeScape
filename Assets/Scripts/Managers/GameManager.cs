using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Material highlightMat;
    [SerializeField] public Material normalMat;
    [SerializeField] public static int totalEnergy;

    public static int currentEnergy;
    public bool isPlayerTurn;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerTurn = true;

        if (totalEnergy == 0)
        {
            totalEnergy = 7;
        }

        RefreshCurrentEnergy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void RefreshCurrentEnergy()
    {
        currentEnergy = totalEnergy;
    }

    public static void RefreshCharacters()
    {
        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().hasAttacked = false;
        }
    }

    public static void ReduceEnergy()
    {
        if (currentEnergy > 0)
        {
            currentEnergy--;
        }
    }

    public static bool HaveEnergy()
    {
        return currentEnergy > 0;
    }

}
