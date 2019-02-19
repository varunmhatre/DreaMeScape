using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAI : MonoBehaviour
{
    [SerializeField] GameObject gameManagerHandler;
    GameManager gameManager;
    GameObject[] pirates;

    List<UnitCoordinates> pirateTurns;

    bool piratesInProgress;
    bool phase1;

    float timer;
    const float timeToWaitForEachMove = 1.0f;

    int selectedPirate;

    private void Start()
    {
        pirateTurns = new List<UnitCoordinates>();
        piratesInProgress = false;
        gameManager = gameManagerHandler.GetComponent<GameManager>();
        selectedPirate = 0;
        phase1 = false;
    }

    public void GetAllPirates()
    {
        pirates = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
        if (!gameManager.isPlayerTurn)
        {
            if (!phase1)
            {
                timer = 0.0f;
                selectedPirate = 0;
                GetAllPirates();
            }
            else if (piratesInProgress)
            {
                PirateAttack();
            }
            else
            {
                //GetPiratesCoordinates
                GetPirateAttackMoves();
            }
        }
    }

    void PirateAttack()
    {
        
    }

    void ProgressToPlayerTurn()
    {
        pirateTurns.Clear();
        timer = 0.0f;
        phase1 = false;
        piratesInProgress = false;
        gameManager.isPlayerTurn = true;
    }

    void GetPirateAttackMoves()
    {
        if (selectedPirate == pirates.Length)
        {
            ProgressToPlayerTurn();
            return;
        }
        UnitCoordinates closestPlayer = FindClosestPlayer();
        //Astar to player
        PopulateTheDestination();
    }

    void PopulateTheDestination()
    {

    }

    UnitCoordinates FindClosestPlayer()
    {
        int shortestDistance = 10000;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        UnitCoordinates piratePosition = pirates[selectedPirate].GetComponent<UnitCoordinates>();
        UnitCoordinates playerPosition = null;
        for (int i = 0; i < players.Length; i++)
        {
            playerPosition = players[i].GetComponent<UnitCoordinates>();
            int distance = Mathf.Abs(playerPosition.x - piratePosition.x) + Mathf.Abs(playerPosition.y - piratePosition.y);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
            }
        }
        return playerPosition;
    }
}