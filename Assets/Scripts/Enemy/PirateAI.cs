using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAI : MonoBehaviour
{
    enum Strategy
    {
        patrol,
        attackClosest,
        protectCaptain
    }

    Strategy strategy;

    List<GridCoordinates> pirateTurns;
    GridCoordinates currentUnitLocaton;
    GameObject alliedCharacter;
    List<GameObject> closeToPirateCaptain;

    bool piratesInProgress;
    bool canPirateAttack;
    bool switchPirate;
    bool endScriptRunning;

    float timer;
    float timerForCameraSwitch;
    [SerializeField] float timeToWaitForEachMove = 1.0f;
    [SerializeField] float timeToWaitForNewPirateToMove = 2.0f;

    CameraFocus cameraMain;
    int selectedPirate;
    Vector3 initialPiratePosition;

    private void Start()
    {
        endScriptRunning = false;
        switchPirate = false;
        cameraMain = Camera.main.GetComponent<CameraFocus>();
        closeToPirateCaptain = new List<GameObject>();
        pirateTurns = new List<GridCoordinates>();
        piratesInProgress = false;
        selectedPirate = 0;
        timer = timeToWaitForNewPirateToMove - 1.0f;
        canPirateAttack = false;
        alliedCharacter = null;
        strategy = Strategy.patrol;
    }

    private void Update()
    {
        if (!GameManager.isPlayerTurn)
        {
            if (piratesInProgress)
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
        if (pirateTurns.Count == 0)
        {
            if (IsPlayerNextToYou(currentUnitLocaton))
            {
                alliedCharacter.GetComponent<Stats>().TakeDamage(CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<Stats>().damage);
                alliedCharacter.GetComponent<Stats>().CheckDeath();
                alliedCharacter = null;
            }
            piratesInProgress = false;
            selectedPirate++;
            switchPirate = true;
            timer = 0.0f;
            return;
        }
        timer += Time.deltaTime;

        //Change pirate's position
        if (pirateTurns[0].GetComponent<GridPiece>().unit == null)
        {
            if (timer < timeToWaitForEachMove)
            {
                CharacterManager.allEnemyCharacters[selectedPirate].transform.position =
                    Vector3.Lerp(initialPiratePosition, pirateTurns[0].transform.position, timer / timeToWaitForEachMove);
                return;
            }
            currentUnitLocaton.transform.GetComponent<GridPiece>().unit = null;
            currentUnitLocaton = pirateTurns[0];
            currentUnitLocaton.transform.GetComponent<GridPiece>().unit = CharacterManager.allEnemyCharacters[selectedPirate];
            CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<UnitCoordinates>().SetUnitCoordinates(currentUnitLocaton.x, currentUnitLocaton.y);
            CharacterManager.allEnemyCharacters[selectedPirate].transform.position = pirateTurns[0].transform.position;
            initialPiratePosition = pirateTurns[0].transform.position;
            pirateTurns.RemoveAt(0);
        }
        //Ocupied spot. End turn now
        else
        {
            pirateTurns.Clear();
        }
        timer = 0.0f;
    }

    IEnumerator ProgressToPlayerTurn()
    {
        yield return new WaitForSeconds(timeToWaitForEachMove);
        closeToPirateCaptain.Clear();
        timer = timeToWaitForNewPirateToMove - 1.0f;
        strategy = Strategy.patrol;
        selectedPirate = 0;
        piratesInProgress = false;
        cameraMain.ResetCamera();
        GameManager.RefreshEnemies();
        GameManager.BeginNewTurn();
        endScriptRunning = false;
    }

    void DetermineStrategy()
    {
        if (strategy == Strategy.patrol)
        {
            foreach (var ally in CharacterManager.allAlliedCharacters)
            {
                if (!ally)
                    continue;
                foreach (var enemy in CharacterManager.allEnemyCharacters)
                {
                    if (AdjacencyHandler.CompareAdjacency(ally, enemy, 2))
                    {
                        strategy = Strategy.protectCaptain;
                        break;
                    }
                }
            }
        }

        if (strategy == Strategy.protectCaptain)
        {
            GameObject pirateCaptain = GetPirateCaptain();
            closeToPirateCaptain.Clear();

            foreach (var item in CharacterManager.allAlliedCharacters)
            {
                if (!item)
                    continue;
                if (AdjacencyHandler.CompareAdjacency(pirateCaptain, item, 4))
                {
                    closeToPirateCaptain.Add(item);
                }
            }

            if (closeToPirateCaptain.Count == 0)
            {
                strategy = Strategy.attackClosest;
            }
        }


    }

    void GetPirateAttackMoves()
    {
        if (selectedPirate == CharacterManager.allEnemyCharacters.Count)
        {
            if (!endScriptRunning)
            {
                endScriptRunning = true;
                StartCoroutine(ProgressToPlayerTurn());
            }
            return;
        }

        DetermineStrategy();

        if (switchPirate)
        {
            timerForCameraSwitch += Time.deltaTime;
            if (timerForCameraSwitch >= timeToWaitForEachMove)
            {
                cameraMain.ChangePirate(CharacterManager.allEnemyCharacters[selectedPirate].transform);
                switchPirate = false;
                timerForCameraSwitch = 0.0f;
            }
        }

        timer += Time.deltaTime;
        if (timer < timeToWaitForNewPirateToMove)
        {
            return;
        }
        timer = 0.0f;

        if (CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<Pirate>().isStunned)
        {
            CharacterManager.allEnemyCharacters[selectedPirate].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            selectedPirate++;
            switchPirate = true;
            return;
        }

        if ((strategy == Strategy.patrol || strategy == Strategy.attackClosest) && CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<PirateCaptain>())
        {
            selectedPirate++;
            switchPirate = true;
            return;
        }

        UnitCoordinates pirateCoordinate = CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<UnitCoordinates>();
        currentUnitLocaton = PathFinding.GetGridFromUnitCoordinate(pirateCoordinate);

        CalculateDestination();

        initialPiratePosition = CharacterManager.allEnemyCharacters[selectedPirate].transform.position;
        piratesInProgress = true;
    }

    bool IsPlayerNextToYou(GridCoordinates currentUnitLocaton)
    {
        UnitCoordinates enemyGrid;
        foreach (var item in CharacterManager.allAlliedCharacters)
        {
            if (!item)
                continue;

            enemyGrid = item.GetComponent<UnitCoordinates>();
            if ((currentUnitLocaton.x < (enemyGrid.x + 2) && currentUnitLocaton.x > (enemyGrid.x - 2)) &&
                 (currentUnitLocaton.y < (enemyGrid.y + 2) && currentUnitLocaton.y > (enemyGrid.y - 2)))
            {
                alliedCharacter = item;
                return true;
            }
        }
        return false;
    }

    void PopulateTheDestination(UnitCoordinates closestPlayer)
    {
        int numberOfTurns = CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<Pirate>().GetNumberOfTurns();
        
        GridCoordinates targetUnitLocation = PathFinding.GetGridFromUnitCoordinate(closestPlayer);

        List<GridCoordinates> wholePath = PathFinding.AStar(currentUnitLocaton, targetUnitLocation);
        numberOfTurns = Mathf.Min(numberOfTurns, wholePath.Count);

        pirateTurns = wholePath.GetRange(0, numberOfTurns);
    }

    void PopulateTheDestinationForPatrol(GridCoordinates toGoTo)
    {
        int numberOfTurns = CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<Pirate>().GetNumberOfTurns()/2;

        Point dest = new Point(toGoTo.x, toGoTo.y);

        GridCoordinates targetUnitLocation = PathFinding.GetGridFromPoint(dest);

        List<GridCoordinates> wholePath = PathFinding.AStar(currentUnitLocaton, targetUnitLocation);
        numberOfTurns = Mathf.Min(numberOfTurns, wholePath.Count);

        pirateTurns = wholePath.GetRange(0, numberOfTurns);
    }

    void CalculateDestination()
    {
        switch (strategy)
        {
            case Strategy.patrol:
                PirateMovementPoints piratePatrol = CharacterManager.allEnemyCharacters[selectedPirate].transform.GetComponent<PirateMovementPoints>();

                GridCoordinates toTarget = null;

                for (int i = 0; i < 2; i++)
                {
                    if (piratePatrol.isMovingPositive)
                    {
                        toTarget = piratePatrol.startPosition;
                        //Astar to targerDestination
                        PopulateTheDestinationForPatrol(toTarget);
                    }
                    else
                    {
                        toTarget = piratePatrol.endPosition;
                        //Astar to targerDestination
                        PopulateTheDestinationForPatrol(toTarget);
                    }
                    if (pirateTurns.Count > 0 && !pirateTurns[0].GetComponent<GridPiece>().unit)
                    {
                        break;
                    }
                    piratePatrol.isMovingPositive = !piratePatrol.isMovingPositive;
                }
                break;
            case Strategy.attackClosest:
                UnitCoordinates targetDestination = FindClosestPlayer(true);
                //Astar to targerDestination
                PopulateTheDestination(targetDestination);
                break;
            case Strategy.protectCaptain:
                UnitCoordinates pirateCaptain = FindClosestPlayer(false);
                //Astar to targerDestination
                PopulateTheDestination(pirateCaptain);
                break;
            default:
                break;
        }
    }

    UnitCoordinates FindClosestPlayer(bool all)
    {
        int shortestDistance = 10000;

        UnitCoordinates piratePosition = CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<UnitCoordinates>();
        UnitCoordinates playerPosition = null;

        List<GameObject> thingsToCheck = closeToPirateCaptain;

        if (all)
        {
            thingsToCheck = CharacterManager.allAlliedCharacters;
        }

        int selectedPlayer = 0;
        for (int i = 0; i < thingsToCheck.Count; i++)
        {
            if (!thingsToCheck[i])
                continue;
            playerPosition = thingsToCheck[i].GetComponent<UnitCoordinates>();
            int distance = Mathf.Abs(playerPosition.x - piratePosition.x) + Mathf.Abs(playerPosition.y - piratePosition.y);

            if (distance < shortestDistance)
            {
                selectedPlayer = i;
                shortestDistance = distance;
            }
        }
        return thingsToCheck[selectedPlayer].GetComponent<UnitCoordinates>();
    } 

    GameObject GetPirateCaptain()
    {
        GameObject pirateCaptain = null;
        for (int i = 0; i < CharacterManager.allEnemyCharacters.Count; i++)
        {
            if (CharacterManager.allEnemyCharacters[i].GetComponent<PirateCaptain>())
            {
                pirateCaptain = CharacterManager.allEnemyCharacters[i];
                break;
            }
        }
        return pirateCaptain;
    }
}