using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonRadius : MonoBehaviour
{
    //Temp. Need a separate cannon highlight
    List<Renderer> gridsToHighlight;

    List<GridPiece> playerTracker;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material initialMaterial;

    // Use this for initialization
    void Start()
    {
        gridsToHighlight = new List<Renderer>();
        playerTracker = new List<GridPiece>();
        GetGridRadius();
        GetAdjacentCells();
    }

    void GetGridRadius()
    {
        //Temp. Need a separate cannon highlight
        UnitCoordinates gamePiece = gameObject.GetComponent<UnitCoordinates>();

        foreach (var grid in GridMatrix.gameGrid)
        {
            if ((grid.x >= (gamePiece.x - CannonStaticVariables.cannonRadius) && grid.x <= (gamePiece.x + CannonStaticVariables.cannonRadius)) &&
                (grid.y >= (gamePiece.y - CannonStaticVariables.cannonRadius) && grid.y <= (gamePiece.y + CannonStaticVariables.cannonRadius)))
            {
                gridsToHighlight.Add(grid.transform.GetComponent<Renderer>());
            }
        }
    }

    void GetAdjacentCells()
    {
        UnitCoordinates gamePiece = gameObject.GetComponent<UnitCoordinates>();

        foreach (var grid in GridMatrix.gameGrid)
        {
            if ((grid.x >= (gamePiece.x - 1) && grid.x <= (gamePiece.x + 1)) &&
                (grid.y >= (gamePiece.y - 1) && grid.y <= (gamePiece.y + 1)))
            {
                playerTracker.Add(grid.transform.GetComponent<GridPiece>());
            }
        }
    }

    public void HighlightGrids()
    {
        foreach (var item in gridsToHighlight)
        {
            //Temp. Need a separate cannon highlight
            //item.GetComponent<GridPieceHighlight>().isHighlighted = true;

            item.material = highlightMaterial;
        }
    }

    public void RemoveHighlights()
    {
        foreach (var item in gridsToHighlight)
        {
            //Temp. Need a separate cannon highlight
            item.material = initialMaterial;
        }
    }

    public bool CheckIfPlayerAround()
    {
        foreach (var item in playerTracker)
        {
            if (item.unit != null && item.unit.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }
}
