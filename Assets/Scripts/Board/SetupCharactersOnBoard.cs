using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SetupCharactersOnBoard : MonoBehaviour
{


    [SerializeField] List<GameObject> characters = new List<GameObject>();
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject pirate;
    [SerializeField] GameObject pirateCaptain;
    int numberOfCannons;

    [SerializeField] GameObject cannon;
    [SerializeField] Transform cannonHandler;

    [SerializeField] GameObject pirate;
    [SerializeField] GameObject pirateCaptain;
   

    // Start is called before the first frame update
    void Start()
    {
        PlaceCharacters();
        PlaceCannons();
    }

    void PlaceCharacters()
    {
        int[] array = { 3,5,   2,5,   1,4,    2,3,   3,3 };
        for (int arrayIndex = 0; arrayIndex < characters.Count; arrayIndex++)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GridCoordinates piece = transform.GetChild(i).GetComponent<GridCoordinates>();
                if ((piece.x == array[arrayIndex * 2]) && (piece.y == ((array[(arrayIndex * 2) + 1]) )))
                {
                    transform.GetChild(i).GetComponent<GridPiece>().unit =
                        Instantiate(characters[arrayIndex], transform.GetChild(i).position, Quaternion.identity);
                   // transform.GetChild(i).GetComponent<GridPiece>().unit.AddComponent<GameObjectEntity>();
                    break;
                }
            }
        }
    }

    void PlaceCannons()
    {
        int[] array = { 7, 7, 7, 1, 11, 7, 11, 1 };
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

                    transform.GetChild(i).GetComponent<GridPiece>().unit = 
                        Instantiate(cannon, pos, rotation, cannonHandler);
                    //transform.GetChild(i).GetComponent<GridPiece>().unit.AddComponent<GameObjectEntity>();
                    break;
                }
            }
        }
    }

    void PlaceCannons()
    {
        int[] array = { 7, 7, 7, 1, 11, 7, 11, 1 };
        numberOfCannons = array.Length / 2;
        for (int arrayIndex = 0; arrayIndex < numberOfCannons; arrayIndex++)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GridCoordinates piece = transform.GetChild(i).GetComponent<GridCoordinates>();
                if ((piece.x == array[arrayIndex * 2]) && (piece.y == ((array[(arrayIndex * 2) + 1]))))
                {
                    transform.GetChild(i).GetComponent<GridPiece>().unit = Instantiate(cannon, transform.GetChild(i).position, Quaternion.identity);
                    transform.GetChild(i).GetComponent<GridPiece>().unit.AddComponent<GameObjectEntity>();
                    break;
                }
            }
        }
    }
}
