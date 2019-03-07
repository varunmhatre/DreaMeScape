using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoardSetup : MonoBehaviour
{
    [SerializeField] public List<GameObject> characters = new List<GameObject>();

    [SerializeField] GameObject cannon;
    [SerializeField] Transform cannonHandler;

    [SerializeField] GameObject pirate;
    [SerializeField] GameObject pirateCaptain;
    [SerializeField] Transform pirateAIHandler;

    [SerializeField] GameObject generator;

    [SerializeField] int levelNum;


    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.allCharacters = new List<GameObject>();
        CharacterManager.allAlliedCharacters = new List<GameObject>();
        CharacterManager.allEnemyCharacters = new List<GameObject>();

        PlaceCharacters(levelNum);
        PlaceCannons(levelNum);
        PlacePirates(levelNum);
        PlacePirateCaptain(levelNum);
        PlaceGenerators(levelNum);
    }

    void PlaceCharacters(int level)
    {
        int[] array = { 3, 4, 4, 4, 5, 4, 2, 4, 1, 4 };
        if (level == 0)
        {
            array = new int[] { 3, 4, 4, 4, 5, 4, 2, 4, 1, 4 };
        }
        else if(level == 1)
        {
            array = new int[] { 3, 5, 2, 5, 4, 4, 2, 3, 3, 3 };
        }
        for (int arrayIndex = 0; arrayIndex < characters.Count; arrayIndex++)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GridCoordinates piece = transform.GetChild(i).GetComponent<GridCoordinates>();
                if ((piece.x == array[arrayIndex * 2]) && (piece.y == ((array[(arrayIndex * 2) + 1]))))
                {
                    characters[arrayIndex].GetComponent<UnitCoordinates>().SetUnitCoordinates(array[arrayIndex * 2], array[(arrayIndex * 2) + 1]);
                    transform.GetChild(i).GetComponent<GridPiece>().unit =
                        Instantiate(characters[arrayIndex], transform.GetChild(i).position, Quaternion.identity);
                    CharacterManager.allAlliedCharacters.Add(transform.GetChild(i).GetComponent<GridPiece>().unit);
                    CharacterManager.allCharacters.Add(transform.GetChild(i).GetComponent<GridPiece>().unit);
                    if (arrayIndex > 1 && level == 0)
                    {
                        CharacterManager.allAlliedCharacters[arrayIndex].SetActive(false);
                    }
                    break;
                }
            }
        }
    }

    void PlaceCannons(int level)
    {
        int[] array = { 0 };
        if (level == 0)
        {
            array = new int[] { 1, 5 };
        }
        else if (level == 1)
        {
            array = new int[] { 7, 7, 7, 1, 11, 7, 11, 1 };
        }
        int numberOfCannons = array.Length / 2;
        for (int arrayIndex = 0; arrayIndex < numberOfCannons; arrayIndex++)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GridCoordinates piece = transform.GetChild(i).GetComponent<GridCoordinates>();
                if ((piece.x == array[arrayIndex * 2]) && (piece.y == ((array[(arrayIndex * 2) + 1]))))
                {
                    Vector3 pos = transform.GetChild(i).position;
                    pos.y += 0.53f;
                    Quaternion rotation = Quaternion.identity;
                    if (piece.y < 4)
                    {
                        rotation.y += 180.0f;
                    }
                    cannon.GetComponent<UnitCoordinates>().SetUnitCoordinates(array[arrayIndex * 2], array[(arrayIndex * 2) + 1]);
                    transform.GetChild(i).GetComponent<GridPiece>().unit =
                        Instantiate(cannon, pos, rotation, cannonHandler);
                    break;
                }
            }
        }
    }

    void PlacePirates(int level)
    {
        int[] array = { 0 };
        if (level == 0)
        {
            array = new int[] { 2, 7, 2, 8 };
        }
        else if (level == 1)
        {
            array = new int[] { 16, 7, 11, 6, 7, 2 };
        }
        int numberOfPirates = array.Length / 2;
        for (int arrayIndex = 0; arrayIndex < numberOfPirates; arrayIndex++)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GridCoordinates piece = transform.GetChild(i).GetComponent<GridCoordinates>();
                if ((piece.x == array[arrayIndex * 2]) && (piece.y == ((array[(arrayIndex * 2) + 1]))))
                {
                    pirate.GetComponent<UnitCoordinates>().SetUnitCoordinates(array[arrayIndex * 2], array[(arrayIndex * 2) + 1]);
                    transform.GetChild(i).GetComponent<GridPiece>().unit =
                        Instantiate(pirate, transform.GetChild(i).position, Quaternion.identity, pirateAIHandler);
                    CharacterManager.allEnemyCharacters.Add(transform.GetChild(i).GetComponent<GridPiece>().unit);
                    CharacterManager.allCharacters.Add(transform.GetChild(i).GetComponent<GridPiece>().unit);
                    if (arrayIndex > -1 && level == 0)
                    {
                        CharacterManager.allEnemyCharacters[arrayIndex].SetActive(false);
                    }
                    break;
                }
            }
        }
    }

    void PlacePirateCaptain(int level)
    {
        int[] array = { 0 };
        if (level == 0)
        {
            array = new int[] { 0 };
        }
        else if (level == 1)
        {
            array = new int[] { 16, 4 };
        }
        int numberOfPirates = array.Length / 2;
        for (int i = 0; i < transform.childCount; i++)
        {
            GridCoordinates piece = transform.GetChild(i).GetComponent<GridCoordinates>();
            if ((piece.x == array[0]) && (piece.y == ((array[1]))))
            {
                pirateCaptain.GetComponent<UnitCoordinates>().SetUnitCoordinates(array[0], array[1]);
                transform.GetChild(i).GetComponent<GridPiece>().unit =
                    Instantiate(pirateCaptain, transform.GetChild(i).position, Quaternion.identity, pirateAIHandler);
                CharacterManager.allEnemyCharacters.Add(transform.GetChild(i).GetComponent<GridPiece>().unit);
                CharacterManager.allCharacters.Add(transform.GetChild(i).GetComponent<GridPiece>().unit);
                break;
            }
        }
    }

    void PlaceGenerators(int level)
    {
        int[] array = { 0 };
        if (level == 0)
        {
            array = new int[] { 4, 7 };
        }
        else if (level == 1)
        {
            array = new int[] { 4, 1, 13, 4 };
        }
        int numberOfGenerators = array.Length / 2;
        for (int arrayIndex = 0; arrayIndex < numberOfGenerators; arrayIndex++)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GridCoordinates piece = transform.GetChild(i).GetComponent<GridCoordinates>();
                if ((piece.x == array[arrayIndex * 2]) && (piece.y == ((array[(arrayIndex * 2) + 1]))))
                {
                    generator.GetComponent<UnitCoordinates>().SetUnitCoordinates(array[arrayIndex * 2], array[(arrayIndex * 2) + 1]);
                    transform.GetChild(i).GetComponent<GridPiece>().unit =
                        Instantiate(generator, transform.GetChild(i).position, Quaternion.identity);
                    break;
                }
            }
        }
    }

    public void AddMorePirates(int numberOfPirates)
    {
        for (int i = 0; i < numberOfPirates; i++)
        {
            foreach (var item in GridMatrix.gameGrid)
            {
                GridPiece gridPiece = item.transform.GetComponent<GridPiece>();
                if (gridPiece.unit != null)
                {
                    continue;
                }
                int x = item.x;
                int y = item.y;
                if (x > 7 && x < 12 && y > 2 && y < 6)
                {
                    pirate.GetComponent<UnitCoordinates>().SetUnitCoordinates(x, y);
                    gridPiece.unit = Instantiate(pirate, item.transform.position, Quaternion.identity, pirateAIHandler);
                    CharacterManager.allEnemyCharacters.Add(gridPiece.unit);
                    CharacterManager.allCharacters.Add(gridPiece.unit);
                    break;
                }
            }
        }
    }
}
