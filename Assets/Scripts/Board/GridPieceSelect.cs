using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPieceSelect : MonoBehaviour
{
    public int[] playerGridPiece;

    // Start is called before the first frame update
    void Start()
    {
        playerGridPiece = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void highlightMoveSpaces(string playerName, bool toHighlight, int[] playerLocation)
    {
        if (toHighlight)
        {
            playerGridPiece[0] = playerLocation[0];
            playerGridPiece[1] = playerLocation[1];
        }

        int[,] playerMov = PlayerMoveSpaces.Player_Movements[playerName];

        //Array that holds movespaces is 0,1,2...
        //The following offsets it to -1,0,1 
        //This helps us get up, down, left, right (and beyond) 
        //to highlight with selected player in the middle
        int y = (playerMov.GetLength(0) - 1) / 2;
        int x = (playerMov.GetLength(1) - 1) / 2;

        for (int i = 0; i < playerMov.GetLength(0); i++)
        {
            for (int j = 0; j < playerMov.GetLength(1); j++)
            {
                if (playerMov[i, j] > 0)
                {
                    GridCoordinates neighborPiece = GetGridPieceCoords((playerGridPiece[0] + (j - x)), (playerGridPiece[1] + (i - y)));
                    if (neighborPiece)
                    {
                        if (toHighlight && !neighborPiece.GetComponent<GridPiece>().isOccupied)
                        {
                            neighborPiece.GetComponent<GridPieceHighlight>().highlightPiece();
                        }
                        else
                        {
                            neighborPiece.GetComponent<GridPieceHighlight>().removeHighlight();
                        }
                    }
                }
            }
        }
    }

    public Transform GetGridPieceOnClick()
    {
        RaycastHit hit = RaycastManager.GetRaycastHitForTag("GridPiece");
        return hit.transform;
    }

    
    //Replace with Burhan's dictionary idea (store int <x,y> against corresponding grid piece)
    public GridCoordinates GetGridPieceCoords(int x, int y)
    {
        foreach (GridCoordinates gc in GridMatrix.gameGrid)
        {
            if (gc.x == x && gc.y == y)
            {
                return gc;
            }
        }
        return null;
    }
}
