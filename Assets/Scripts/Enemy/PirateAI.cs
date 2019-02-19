using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAI : MonoBehaviour
{
    enum Direction
    {
        up,
        right,
        down,
        left
    }

    Direction priority;

    [SerializeField] GameObject gameManagerHandler;
    GameManager gameManager;
    GameObject[] pirates;

    List<GridCoordinates> pirateTurns;

    bool piratesInProgress;
    bool phase1;

    float timer;
    const float timeToWaitForEachMove = 1.0f;

    int selectedPirate;

    private void Start()
    {
        pirateTurns = new List<GridCoordinates>();
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
        foreach (var pirate in pirates)
        {
            pirate.GetComponent<Pirate>().isStunned = false;
        }
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
        PopulateTheDestination(closestPlayer);
    }

    void GetPriority(GridCoordinates currentUnitLocaton, GridCoordinates targetUnitLocation)
    {
        if (Mathf.Abs(currentUnitLocaton.x - targetUnitLocation.x) > Mathf.Abs(currentUnitLocaton.y - targetUnitLocation.y))
        {
            priority = targetUnitLocation.x > currentUnitLocaton.x ? Direction.right : Direction.left;
        }
        else
        {
            priority = targetUnitLocation.y > currentUnitLocaton.y ? Direction.up : Direction.down;
        }
    }

    GridCoordinates GetTheGrid(UnitCoordinates unit)
    {
        GridCoordinates unitLocation = new GridCoordinates();
        foreach (var item in GridMatrix.gameGrid)
        {
            if (item.x == unit.x && item.y == unit.y)
            {
                unitLocation = item;
                break;
            }
        }
        return unitLocation;
    }

    void PopulateTheDestination(UnitCoordinates closestPlayer)
    {
        int count = 0;
        int numberOfTurns = pirates[selectedPirate].GetComponent<Pirate>().noOfTurns;
        UnitCoordinates pirateCoordinate = pirates[selectedPirate].GetComponent<UnitCoordinates>();
        bool canMoveLeft, canMoveRight, canMoveUp, canMoveDown;

        GridCoordinates currentUnitLocaton = GetTheGrid(pirateCoordinate);
        GridCoordinates targetUnitLocation = GetTheGrid(closestPlayer);

        GetPriority(currentUnitLocaton, targetUnitLocation);

        while (count <= 30 ||
        pirateTurns.Count == numberOfTurns)
        {
            //Reached target
            if ((pirateTurns[0].GetComponent<GridCoordinates>().x == pirateCoordinate.x &&
            pirateTurns[0].GetComponent<GridCoordinates>().y == pirateCoordinate.y))
            {
                break;
            }

            canMoveDown = canMoveLeft = canMoveRight = canMoveUp = true;

            if (priority == Direction.right && canMoveRight)
            {

            }
            
        }
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