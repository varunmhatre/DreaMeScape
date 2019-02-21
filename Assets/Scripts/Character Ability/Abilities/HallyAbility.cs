using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallyAbility : MonoBehaviour
{
    [SerializeField] GameObject pigPrefab;
    [SerializeField] GameObject birdPrefab;
    UnitCoordinates unitCoordinate;

    private void Start()
    {
        unitCoordinate = GetComponent<UnitCoordinates>();
    }

    void AddPig()
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
                pigPrefab.GetComponent<UnitCoordinates>().SetUnitCoordinates(x, y);
                gridPiece.unit = Instantiate(pigPrefab, item.transform.position, Quaternion.identity);
                CharacterManager.allHallyCharacters.Add(gridPiece.unit);
                break;
            }
        }
    }

    void AddBird()
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
                birdPrefab.GetComponent<UnitCoordinates>().SetUnitCoordinates(x, y);
                gridPiece.unit = Instantiate(birdPrefab, item.transform.position, Quaternion.identity);
                CharacterManager.allHallyCharacters.Add(gridPiece.unit);
                CharacterManager.allCharacters.Add(gridPiece.unit);
                break;
            }
        }
    }
}
