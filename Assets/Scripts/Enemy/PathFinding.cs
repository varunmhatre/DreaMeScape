using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public static void PathFind(GridCoordinates startNode, GridCoordinates targetNode, List<GridCoordinates> path)
    {
        while (targetNode != startNode)
        {
            path.Insert(0, targetNode);
            targetNode = targetNode.parentNode;
        }
    }

    private static float Euclidean(GridCoordinates start, GridCoordinates end)
    {
        var x = start.x - end.x;
        var y = start.y - end.y;

        return Mathf.Sqrt(x * x + y * y);
    }

    private static int Partition(List<GridCoordinates> nodes, int low, int high)
    {
        GridCoordinates pivot = nodes[high];

        // index of smaller element 
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            // If current element is smaller  
            // than or equal to pivot 
            if (nodes[j].fCost <= pivot.fCost)
            {
                i++;

                // swap arr[i] and arr[j] 
                GridCoordinates temp = nodes[i];
                nodes[i] = nodes[j];
                nodes[j] = temp;
            }
        }

        // swap arr[i+1] and arr[high] (or pivot) 
        GridCoordinates temp1 = nodes[i + 1];
        nodes[i + 1] = nodes[high];
        nodes[high] = temp1;

        return i + 1;
    }

    static void QuickSort(List<GridCoordinates> nodes, int low, int high)
    {
        if (low < high)
        {
            /* pi is partitioning index, arr[pi] is  
            now at right place */
            int pi = Partition(nodes, low, high);

            // Recursively sort elements before 
            // partition and after partition 
            QuickSort(nodes, low, pi - 1);
            QuickSort(nodes, pi + 1, high);
        }
    }

    public static List<GridCoordinates> AStar(GridCoordinates startNode, GridCoordinates targetNode)
    {
        List<GridCoordinates> path = new List<GridCoordinates>();
        List<GridCoordinates> openSet = new List<GridCoordinates>();
        HashSet<GridCoordinates> closedSet = new HashSet<GridCoordinates>();
        bool wasSuccessful = false;
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            QuickSort(openSet, 0, openSet.Count - 1);
            GridCoordinates currentNode = openSet[0];
            openSet.RemoveAt(0);

            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                wasSuccessful = true;
                break;
            }

            foreach (GridCoordinates neighbor in currentNode.GetComponent<ConnectingGrids>().connectingGrids)
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float newMoveCostToNeighbor = currentNode.gCost + Euclidean(currentNode, neighbor);
                
                //Add loads of weight if blocked
                if (neighbor.GetComponent<GridPiece>().unit)
                    newMoveCostToNeighbor += 5.0f;

                if (newMoveCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMoveCostToNeighbor;
                    neighbor.hCost = Euclidean(neighbor, targetNode);
                    neighbor.parentNode = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        if (wasSuccessful)
        {
            PathFind(startNode, targetNode, path);
        }

        NullifyAllParent();
        return path;
    }

    private static void NullifyAllParent()
    {
        foreach (var node in GridMatrix.gameGrid)
        {
            node.parentNode = null;
            node.gCost = 0.0f;
            node.hCost = 0.0f;
        }
    }

   public static GridCoordinates GetGridFromUnitCoordinate(UnitCoordinates unit)
    {
        foreach (var item in GridMatrix.gameGrid)
        {
            if (item.x == unit.x && item.y == unit.y)
            {
                return item;
            }
        }
        return null;
    }
}