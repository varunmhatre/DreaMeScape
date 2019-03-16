using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GridCoordinates : MonoBehaviour {
    /// <summary>
    /// These are to be modified by external scripts/references
    /// </summary>
    public int x = 0;
    public int y = 0;

    /// <summary>
    /// For Astar
    /// </summary>
    public GridCoordinates parentNode;
    public List<GridCoordinates> connectingGrids;

    public float gCost;
    public float fCost;
    public float hCost;
}