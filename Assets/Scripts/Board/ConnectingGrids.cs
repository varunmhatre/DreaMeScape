using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ConnectingGrids : MonoBehaviour
{
    public List<GridCoordinates> connectingGrids;
    void Start()
    {
        GridCoordinates currentGrid = GetComponent<GridCoordinates>();
        foreach (var item in GridMatrix.gameGrid)
        {
            if (((item.x == (currentGrid.x + 1) || item.x == (currentGrid.x - 1)) && item.y == currentGrid.y) ||
                 ((item.y == (currentGrid.y + 1) || item.y == (currentGrid.y - 1)) && item.x == currentGrid.x))
            {
                connectingGrids.Add(item);
            }
        }
    }
}
