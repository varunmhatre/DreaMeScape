using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Material highlightMat;
    [SerializeField] public Material normalMat;
    [SerializeField] public static int totalEnergy;

    public static int currentEnergy;
    public static bool isPlayerTurn;
    public static int roundCounter;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerTurn = true;

        if (totalEnergy == 0)
        {
            totalEnergy = 7;
        }

        RefreshCurrentEnergy();
        roundCounter = 0;
    }

    public static void EndCurrentTurn()
    {
        isPlayerTurn = false;
    }

    public static void BeginNewTurn()
    {
        roundCounter++;
        isPlayerTurn = true;
        RefreshCurrentEnergy();
        RefreshCharacters();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void RefreshCurrentEnergy()
    {
        currentEnergy = totalEnergy;
    }

    public static void RefreshEnemies()
    {
        foreach (var enemy in CharacterManager.allEnemyCharacters)
        {
            enemy.GetComponent<Stats>().hasAttacked = false;
            enemy.GetComponent<Pirate>().isStunned = false;
        }
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
