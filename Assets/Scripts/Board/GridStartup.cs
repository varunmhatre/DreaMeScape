using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is only used in startup
/// </summary>
public class GridStartup : MonoBehaviour {
    [SerializeField] GameObject gridPrefab;
    int maxX;
    int maxY;
    float widthOfGrid;

    // Use this for initialization
    void Start()
    {
        maxX = 31;
        maxY = 13;

        //As the prefab needs to be symmetrical accross x and z, 
        //we can choose any one of the two
        widthOfGrid = gridPrefab.transform.localScale.x;

        CreateGrid();
    }

    void CreateGrid()
    {
        GridMatrix.gameGrid = new GameObject[maxX, maxY];

        Vector3 placementOrigin = Vector3.zero;
        Vector3 directionVector = -Vector3.up;

        //LayerMasking every component
        int layerMask = LayerMask.GetMask("GridArea");

        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                placementOrigin = new Vector3((float)x * widthOfGrid, 20, (float)y * widthOfGrid);

                RaycastHit hit;
                if (Physics.Raycast(placementOrigin, directionVector, out hit, Mathf.Infinity, layerMask))
                {
                    GridMatrix.gameGrid[x, y] = Instantiate(gridPrefab, hit.point, Quaternion.identity, transform);
                }
                else
                {
                    GridMatrix.gameGrid[x, y] = Instantiate(gridPrefab, placementOrigin, Quaternion.identity, transform);
                    GridMatrix.gameGrid[x, y].GetComponent<MeshRenderer>().enabled = false;
                    GridMatrix.gameGrid[x, y].GetComponent<BoxCollider>().enabled = false;
                    GridMatrix.gameGrid[x, y].GetComponent<GridPiece>().isDead = true;
                }
                GridMatrix.gameGrid[x, y].name = "GridX" + x + "Y" + y;
                GridMatrix.gameGrid[x, y].GetComponent<GridCoordinates>().x = x;
                GridMatrix.gameGrid[x, y].GetComponent<GridCoordinates>().y = y;
            }
        }
    }
}
