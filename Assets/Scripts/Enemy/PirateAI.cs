using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAI : MonoBehaviour
{
    enum Strategy
    {
        patrol,
        groupUp,
        attackClosest,
        protectCaptain
    }

    Strategy strategy;

    List<GridCoordinates> pirateTurns;
    GridCoordinates currentUnitLocaton;
    GameObject alliedCharacter;

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
        if (timer < timeToWaitForEachMove)
        {
            CharacterManager.allEnemyCharacters[selectedPirate].transform.position =
                Vector3.Lerp(initialPiratePosition, pirateTurns[0].transform.position, timer / timeToWaitForEachMove);
            return;
        }

        //Change pirate's position
        if (pirateTurns[0].GetComponent<GridPiece>().unit == null)
        {
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
        timer = timeToWaitForNewPirateToMove - 1.0f;
        selectedPirate = 0;
        piratesInProgress = false;
        cameraMain.ResetCamera();
        GameManager.RefreshEnemies();
        GameManager.BeginNewTurn();
        endScriptRunning = false;
    }

    void DetermineStrategy()
    {

    }

    void GetPirateAttackMoves()
    {
        if (selectedPirate == 0 || strategy == Strategy.patrol)
        {
            DetermineStrategy();
        }
        if (selectedPirate == CharacterManager.allEnemyCharacters.Count)
        {
            if (!endScriptRunning)
            {
                endScriptRunning = true;
                StartCoroutine(ProgressToPlayerTurn());
            }
            return;
        }

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
            selectedPirate++;
            return;
        }

        if (strategy == Strategy.patrol && CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<PirateCaptain>())
        {
            selectedPirate++;
            return;
        }

        //UnitCoordinates targetDestination = FindClosestPlayer();

        //Astar to targerDestination
        //PopulateTheDestination(targetDestination);
        // UnitCoordinates targetDestination = GetTarget();
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

    void CalculateDestination()
    {
        switch (strategy)
        {
            case Strategy.patrol:
                PirateMovementPoints piratePatrol = CharacterManager.allEnemyCharacters[selectedPirate].transform.GetComponent<PirateMovementPoints>();
                if (piratePatrol.isMovingPositive)
                {
                    Point destination = new Point(currentUnitLocaton.x, currentUnitLocaton.y + 1);
                    GridCoordinates gridToMoveTo = PathFinding.GetGridFromPoint(destination);
                    pirateTurns.Add(gridToMoveTo);
                    if (destination.y == piratePatrol.endPosition.y)
                    {
                        piratePatrol.isMovingPositive = false;
                    }
                }
                else
                {
                    Point destination = new Point(currentUnitLocaton.x, currentUnitLocaton.y - 1);
                    GridCoordinates gridToMoveTo = PathFinding.GetGridFromPoint(destination);
                    pirateTurns.Add(gridToMoveTo);
                    if (destination.y == piratePatrol.endPosition.y)
                    {
                        piratePatrol.isMovingPositive = false;
                    }
                }

                break;
            case Strategy.groupUp:
                break;
            case Strategy.attackClosest:
                break;
            case Strategy.protectCaptain:
                break;
            default:
                break;
        }
    }

    UnitCoordinates FindClosestPlayer()
    {
        int shortestDistance = 10000;

        UnitCoordinates piratePosition = CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<UnitCoordinates>();
        UnitCoordinates playerPosition = null;
        int selectedPlayer = 0;
        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            if (!CharacterManager.allAlliedCharacters[i])
                continue;
            playerPosition = CharacterManager.allAlliedCharacters[i].GetComponent<UnitCoordinates>();
            int distance = Mathf.Abs(playerPosition.x - piratePosition.x) + Mathf.Abs(playerPosition.y - piratePosition.y);

            if (distance < shortestDistance)
            {
                selectedPlayer = i;
                shortestDistance = distance;
            }
        }
        return CharacterManager.allAlliedCharacters[selectedPlayer].GetComponent<UnitCoordinates>();
    }

    UnitCoordinates LocateTargetLocation()
    {
        return null;
    }

}
