using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AdjacencyHandler
{ 
    public static int NumPlayerCharactersAround(GameObject obj, int distance)
    {
        int count = 0;

        for(int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {

            int xPosPlyr = -100;
            int yPosPlyr = -100;

            if (CharacterManager.allAlliedCharacters[i].GetComponent<UnitCoordinates>() != null)
            {
                xPosPlyr = CharacterManager.allAlliedCharacters[i].GetComponent<UnitCoordinates>().x;
                yPosPlyr = CharacterManager.allAlliedCharacters[i].GetComponent<UnitCoordinates>().y;
            }
            else
            {
                return -1;
            }

            int xPosObj = -100;
            int yPosObj = -100;

            if (obj.GetComponent<UnitCoordinates>() != null)
            {
                xPosObj = obj.GetComponent<UnitCoordinates>().x;
                yPosObj = obj.GetComponent<UnitCoordinates>().y;
            }
            //something broke
            else
            {
                return -1;
            }

            if (xPosPlyr <= xPosObj + distance && xPosPlyr >= xPosObj - distance
                    && yPosPlyr <= yPosObj + distance && yPosPlyr >= yPosObj - distance)
            {
                count++;
            }
        }

        return count;
    }    

    public static bool CompareAdjacency(GameObject obj1, GameObject obj2, int distance)
    {
        int xPos1 = 100;
        int yPos1 = 100;

        int xPos2 = -100;
        int yPos2 = -100;

        if (obj1.GetComponent<UnitCoordinates>() && obj2.GetComponent<UnitCoordinates>())
        {
            UnitCoordinates obj1Coord = obj1.GetComponent<UnitCoordinates>();
            UnitCoordinates obj2Coord = obj2.GetComponent<UnitCoordinates>();

            xPos1 = obj1Coord.x;
            yPos1 = obj1Coord.y;

            xPos2 = obj2Coord.x;
            yPos2 = obj2Coord.y;
        }
        else if (obj1.GetComponent<UnitCoordinates>() && obj2.GetComponent<GridCoordinates>())
        {
            UnitCoordinates obj1Coord = obj1.GetComponent<UnitCoordinates>();
            GridCoordinates obj2Coord = obj2.GetComponent<GridCoordinates>();
            xPos1 = obj1Coord.x;
            yPos1 = obj1Coord.y;

            xPos2 = obj2Coord.x;
            yPos2 = obj2Coord.y;
        }

        if (xPos1 <= xPos2 + distance && xPos1 >= xPos2 - distance
                    && yPos1 <= yPos2 + distance && yPos1 >= yPos2 - distance)
        {
            return true;
        }

        return false;
    }
}
