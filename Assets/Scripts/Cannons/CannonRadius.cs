using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRadius : MonoBehaviour
{
    List<Transform> gridsToHighlight;

    // Use this for initialization
    void Start()
    {
        gridsToHighlight = new List<Transform>();
        GetGridRadius();
    }

    void GetGridRadius()
    {
        UnitCoordinates gamePiece = gameObject.GetComponent<UnitCoordinates>();

        foreach (var grid in GridMatrix.gameGrid)
        {
            if ((grid.x > (gamePiece.x - CannonStaticVariables.cannonRadius) && grid.x < (gamePiece.x + CannonStaticVariables.cannonRadius)) &&
                (grid.y > (gamePiece.y - CannonStaticVariables.cannonRadius) && grid.y < (gamePiece.y + CannonStaticVariables.cannonRadius)))
            {
                gridsToHighlight.Add(grid.transform);
            }
        }
    }

    public void HighlightGrids()
    {
        foreach (var item in gridsToHighlight)
        {
            item.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void RemoveHighlights()
    {
        foreach (var item in gridsToHighlight)
        {
            item.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
