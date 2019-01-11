using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

//position
public class GridSystem : ComponentSystem {

    private struct gridObjects{
        public GridPiece pieceData;
        public GridCoordinates gridCoordinate;
    }

    protected override void OnUpdate()
    {
        //This checks all the tiles
        //Example for using other stuff with systems
        foreach (var item in GetEntities<gridObjects>())
        {
        }
    }
}
