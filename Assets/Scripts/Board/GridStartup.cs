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
        Vector3 placementOrigin = Vector3.zero;
        Vector3 directionVector = -Vector3.up;

        //LayerMasking every component
        int layerMask = LayerMask.GetMask("GridArea");
        GameObject gameObject;
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                placementOrigin = new Vector3((float)x * widthOfGrid, 20, (float)y * widthOfGrid);

                RaycastHit hit;
                if (Physics.Raycast(placementOrigin, directionVector, out hit, Mathf.Infinity, layerMask))
                {
                    gameObject = Instantiate(gridPrefab, hit.point, Quaternion.identity, transform);
                }
                else
                {
                    gameObject = Instantiate(gridPrefab, placementOrigin, Quaternion.identity, transform);
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    gameObject.GetComponent<GridPiece>().isDead = true;
                }
                gameObject.name = "GridX" + x + "Y" + y;
                gameObject.GetComponent<GridCoordinates>().x = x;
                gameObject.GetComponent<GridCoordinates>().y = y;
            }
        }
    }
}
