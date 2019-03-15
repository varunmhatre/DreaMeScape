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
            return;
        }
        CharacterManager.allEnemyCharacters[selectedPirate].transform.position = pirateTurns[0].transform.position;
        pirateTurns.RemoveAt(0);
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
        
        //Setup the Coordinates
        if (pirateTurns.Count > 0)
        {
            piratesInProgress = true;
            currentUnitLocaton.transform.GetComponent<GridPiece>().unit = null;
            currentUnitLocaton = pirateTurns[0];
            pirateTurns.RemoveAt(pirateTurns.Count - 1);
            pirateTurns[0].transform.GetComponent<GridPiece>().unit = CharacterManager.allEnemyCharacters[selectedPirate];
            CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<UnitCoordinates>().SetUnitCoordinates(currentUnitLocaton.x, currentUnitLocaton.y);
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

    void PopulateTheDestination(UnitCoordinates closestPlayer)
    {
        int numberOfTurns = CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<Pirate>().GetNumberOfTurns();
        UnitCoordinates pirateCoordinate = CharacterManager.allEnemyCharacters[selectedPirate].GetComponent<UnitCoordinates>();

        currentUnitLocaton = PathFinding.GetGridFromUnitCoordinate(pirateCoordinate);
        GridCoordinates targetUnitLocation = PathFinding.GetGridFromUnitCoordinate(closestPlayer);

        pirateTurns = PathFinding.AStar(currentUnitLocaton, targetUnitLocation);
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
