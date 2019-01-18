using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SetupCharactersOnBoard : MonoBehaviour
{


    [SerializeField] List<GameObject> characters = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        PlaceCharacters();
    }

    void PlaceCharacters()
    {
        int[] array = { 3, 5, 2, 5, 2, 4, 2, 3, 3, 3 };
        for (int arrayIndex = 0; arrayIndex < characters.Count; arrayIndex++)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GridCoordinates piece = transform.GetChild(i).GetComponent<GridCoordinates>();
                if ((piece.x == array[arrayIndex * 2]) && (piece.y == ((array[(arrayIndex * 2) + 1]) )))
                {
                    transform.GetChild(i).GetComponent<GridPiece>().unit = Instantiate(characters[arrayIndex], transform.GetChild(i).position, Quaternion.identity);
                    break;
                }
            }
        }
    }
}
