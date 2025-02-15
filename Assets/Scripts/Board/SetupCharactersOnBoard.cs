﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SetupCharactersOnBoard : MonoBehaviour
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

        PlaceCharacters(1);
        PlaceCannons(1);
        PlacePirates(1);
        PlacePirateCaptain(1);
        PlaceGenerators(1);
    }

    void PlaceCharacters(int level)
    {
        int[] array = { 3, 5, 2, 5, 1, 4, 2, 3, 3, 3 };
        if (level == 1)
        {
            array = new int[] { 3, 5, 2, 5, 3, 3, 2, 3, 3, 3 };
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
                    break;
                }
            }
        }
    }

    void PlaceCannons(int level)
    {
        int[] array = { 7, 7, 7, 1, 11, 7, 11, 1 };
        if (level == 1)
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
        int[] array = { 16, 7, 11, 6, 7, 2 };
        if (level == 1)
        {
            array = new int[] { 16, 2, 13, 6, 9, 2 };
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
                    break;
                }
            }
        }
    }

    void PlacePirateCaptain(int level)
    {
        int[] array = { 16, 4 };
        if (level == 1)
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
        int[] array = { 3, 1, 6, 4};
        if (level == 1)
        {
            array = new int[] { 3, 1, 6, 4 };
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
                if (x > 7 && x < 12 && y > 2 && y < 6 )
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
