using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateMovementPoints : MonoBehaviour
{
    public GridCoordinates startPosition;
    public GridCoordinates endPosition;
    public bool isMovingPositive;

    private void Start()
    {
        startPosition = PathFinding.GetGridFromUnitCoordinate(transform.GetComponent<UnitCoordinates>());
        if (gameObject.GetComponent<PirateCaptain>())
        {
            startPosition = endPosition;
        }
        else
        {
            //For level1
            Point endPoint = new Point(startPosition.x, ((7 - startPosition.y) + 1));
            endPosition = PathFinding.GetGridFromPoint(endPoint);

            if (startPosition.y < endPosition.y)
            {
                isMovingPositive = true;
            }
        }
    }
}
