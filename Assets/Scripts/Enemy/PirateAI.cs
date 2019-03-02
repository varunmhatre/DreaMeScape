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

    enum Strategy
    {
        groupUp,
        attackClosest,
        siegeOnOne
    }

    Direction priority;
    Strategy strategy;

    List<GridCoordinates> pirateTurns;
    GridCoordinates currentUnitLocaton;
    GameObject alliedCharacter;

    bool piratesInProgress;
    bool canPirateAttack;
    bool switchPirate;

    float timer;
    float timerForCameraSwitch;
    const float timeToWaitForEachMove = 1.0f;
    const float timeToWaitForNewPirateToMove = 2.0f;

    CameraFocus cameraMain;

    int selectedPirate;

    private void Start()
    {
        switchPirate = false;
        cameraMain = Camera.main.GetComponent<CameraFocus>();
        pirateTurns = new List<GridCoordinates>();
        piratesInProgress = false;
        selectedPirate = 0;
        timer = 0.0f;
        canPirateAttack = false;
        alliedCharacter = null;
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
            timer = 0.0f;
            return;
        }
        timer += Time.deltaTime;
        if (timer < timeToWaitForEachMove)
        {
            return;
        }
        CharacterManager.allEnemyCharacters[selectedPirate].transform.position = pirateTurns[pirateTurns.Count - 1].transform.position;
        pirateTurns.RemoveAt(pirateTurns.Count - 1);
        switchPirate = true;
        timer = 0.0f;
    }

    void ProgressToPlayerTurn()
    {
        timer = 0.0f;
        selectedPirate = 0;
        piratesInProgress = false;
        cameraMain.ResetCamera();
        GameManager.RefreshEnemies();
        GameManager.BeginNewTurn();
    }

    void GetPirateAttackMoves()
    {
        if (selectedPirate == CharacterManager.allEnemyCharacters.Count)
        {
            ProgressToPlayerTurn();
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

        UnitCoordinates closestPlayer = FindClosestPlayer();
        //Astar to player
        PopulateTheDestination(closestPlayer);
        
        piratesInProgress = true;

        //Setup the Coordinates
        if (pirateTurns.Count > 1)
        {
            currentUnitLocaton.transform.GetComponent<GridPiece>().unit = null;
            currentUnitLocaton = pirateTurns[0];
            pirateTurns.RemoveAt(pirateTurns.Count - 1);
            pirateTurns[0].transform.GetComponent<GridPiece>().unit = CharacterManager.allEnemyCharacters[selectedPirate];
            CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<UnitCoordinates>().SetUnitCoordinates(currentUnitLocaton.x, currentUnitLocaton.y);
        }
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

    bool IsPlayerNextToYou(GridCoordinates currentUnitLocaton)
    {
        foreach (var item in CharacterManager.allAlliedCharacters)
        {
            if ((Mathf.Abs(currentUnitLocaton.x - item.GetComponent<UnitCoordinates>().x) + 
                Mathf.Abs(currentUnitLocaton.y - item.GetComponent<UnitCoordinates>().y) == 1))
            {
                alliedCharacter = item;
                return true;
            }
        }

        return false;
    }

    GridCoordinates GetTheGrid(UnitCoordinates unit)
    {
        foreach (var item in GridMatrix.gameGrid)
        {
            if (item.x == unit.x && item.y == unit.y)
            {
                return item;
            }
        }
        return null;
    }

    void PopulateTheDestination(UnitCoordinates closestPlayer)
    {
        int numberOfTurns = CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<Pirate>().GetNumberOfTurns();
        UnitCoordinates pirateCoordinate = CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<UnitCoordinates>();
        bool canMoveLeft, canMoveRight, canMoveUp, canMoveDown;
        canMoveDown = canMoveLeft = canMoveRight = canMoveUp = true;
        bool repeatingNodes = false;

        currentUnitLocaton = GetTheGrid(pirateCoordinate);

        GridCoordinates targetUnitLocation = GetTheGrid(closestPlayer);

        GetPriority(currentUnitLocaton, targetUnitLocation);
        pirateTurns.Insert(0, currentUnitLocaton);


        //Populate the movement array
        while (pirateTurns.Count <= numberOfTurns && !repeatingNodes)
        {
            if (IsPlayerNextToYou(pirateTurns[0]))
            {
                break;
            }

            GridCoordinates gridToGoTo;
            UnitCoordinates gridToFind = new UnitCoordinates();

            gridToFind.SetUnitCoordinates(pirateTurns[0].x, pirateTurns[0].y);
            if (priority == Direction.right && canMoveRight)
            {
                gridToFind.x += 1;
                gridToGoTo = GetTheGrid(gridToFind);
                if (gridToGoTo && !gridToGoTo.transform.GetComponent<GridPiece>().unit)
                {
                    canMoveDown = canMoveLeft = canMoveRight = canMoveUp = true;
                    pirateTurns.Insert(0, gridToGoTo);
                    GetPriority(pirateTurns[0], targetUnitLocation);
                }
                else
                {
                    canMoveRight = false;
                    if (canMoveUp)
                    {
                        priority = Direction.up;
                    }
                    if (canMoveDown)
                    {
                        priority = Direction.down;
                    }
                    else if (canMoveLeft)
                    {
                        priority = Direction.left;
                    }
                }
            }
            else if (priority == Direction.up && canMoveUp)
            {
                gridToFind.y += 1;
                gridToGoTo = GetTheGrid(gridToFind);
                if (gridToGoTo && !gridToGoTo.transform.GetComponent<GridPiece>().unit)
                {
                    canMoveDown = canMoveLeft = canMoveRight = canMoveUp = true;
                    pirateTurns.Insert(0, gridToGoTo);
                    GetPriority(pirateTurns[0], targetUnitLocation);
                }
                else
                {
                    canMoveUp = false;
                    if (canMoveLeft)
                    {
                        priority = Direction.left;
                    }
                    if (canMoveRight)
                    {
                        priority = Direction.right;
                    }
                    else if (canMoveDown)
                    {
                        priority = Direction.down;
                    }
                }
            }
            else if (priority == Direction.left && canMoveLeft)
            {
                gridToFind.x -= 1;
                gridToGoTo = GetTheGrid(gridToFind);
                if (gridToGoTo && !gridToGoTo.transform.GetComponent<GridPiece>().unit)
                {
                    canMoveDown = canMoveLeft = canMoveRight = canMoveUp = true;
                    pirateTurns.Insert(0, gridToGoTo);
                    GetPriority(pirateTurns[0], targetUnitLocation);
                }
                else
                {
                    canMoveLeft = false;
                    if (canMoveUp)
                    {
                        priority = Direction.up;
                    }
                    else if (canMoveDown)
                    {
                        priority = Direction.down;
                    }
                    else if (canMoveRight)
                    {
                        priority = Direction.right;
                    }
                }
            }
            else if (priority == Direction.down && canMoveDown)
            {
                gridToFind.y -= 1;
                gridToGoTo = GetTheGrid(gridToFind);
                if (gridToGoTo && !gridToGoTo.transform.GetComponent<GridPiece>().unit)
                {
                    canMoveDown = canMoveLeft = canMoveRight = canMoveUp = true;
                    pirateTurns.Insert(0, gridToGoTo);
                    GetPriority(pirateTurns[0], targetUnitLocation);
                }
                else
                {
                    canMoveDown = false;
                    if (canMoveLeft)
                    {
                        priority = Direction.left;
                    }
                    else if (canMoveRight)
                    {
                        priority = Direction.right;
                    }
                    else if (canMoveUp)
                    {
                        priority = Direction.up;
                    }
                }
            }
            else
            {
                //Nowhere to go. Blocked.
                break;
            }
            
            //Avoid moving back and forth
            for (int i = 1; i < pirateTurns.Count; i++)
            {
                if (pirateTurns[i] == pirateTurns[0])
                {
                    pirateTurns.RemoveAt(0);
                    repeatingNodes = true;
                    break;
                }
            }
        }

        piratesInProgress = true;
    }

    UnitCoordinates FindClosestPlayer()
    {
        int shortestDistance = 10000;

        UnitCoordinates piratePosition = CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<UnitCoordinates>();
        UnitCoordinates playerPosition = null;
        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            playerPosition = CharacterManager.allAlliedCharacters[i].GetComponent<UnitCoordinates>();
            int distance = Mathf.Abs(playerPosition.x - piratePosition.x) + Mathf.Abs(playerPosition.y - piratePosition.y);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
            }
        }
        return playerPosition;
    }
}