using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMatrix : MonoBehaviour {

    public static List<GridCoordinates> gameGrid;

    private void Start()
    {
        gameGrid = new List<GridCoordinates>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
                gameGrid.Add(transform.GetChild(i).GetComponent<GridCoordinates>());
        }
    }
}