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
            if (item == currentGrid)
                continue;
            if (currentGrid.x < (item.x + 2) && currentGrid.x > (item.x - 2) &&
                 currentGrid.y < (item.y + 2) && currentGrid.y > (item.y - 2))
            {
                connectingGrids.Add(item);
            }
        }
    }
}
