using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPieceSelect : MonoBehaviour
{
    public int[] playerGridPiece;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void highlightMoveSpaces(string playerName, bool toHighlight, int[] playerLocation)
    {
        if (toHighlight)
        {
            playerGridPiece = playerLocation;
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
                    GameObject neighborPiece = GameObject.Find("GridX" + (playerGridPiece[0] + (j - x)) + "Y" + (playerGridPiece[1] + (i - y)));
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
        RaycastHit hit = GetComponent<RaycastManager>().GetRaycastHitForTag("GridPiece");
        return hit.transform;
    }

}
