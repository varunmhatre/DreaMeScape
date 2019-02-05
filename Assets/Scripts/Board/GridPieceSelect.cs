using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPieceSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void highlightMoveSpaces(string playerName)
    {
        int[] playerCoords = getGridPieceCoordsOnClick();
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
                    GameObject neighborPiece = GameObject.Find("GridX" + (playerCoords[0] + (j - x)) + "Y" + (playerCoords[1] + (i - y)));
                    Debug.Log(neighborPiece);
                    if (neighborPiece)
                    {
                        neighborPiece.GetComponent<GridPieceHighlight>().highlightPiece();
                    }
                }
            }
        }
    }

    public int[] getGridPieceCoordsOnClick()
    {
        int[] coords = { -1, -1 };
        RaycastHit hit = GetComponent<RaycastManager>().getRaycastHitForTag("GridPiece");
        if (hit.transform != null)
        {
            coords[0] = hit.transform.GetComponent<GridCoordinates>().x;
            coords[1] = hit.transform.GetComponent<GridCoordinates>().y;
        }

        return coords;
    }

}
