using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ZoneCalculator
{
    
    public static bool CheckIfPositionHasChanged(Vector3 gridPosition, Vector3 previousPosition, GridStructure grid)
    {
        return Vector3Int.FloorToInt(grid.CalculateGridPosition(gridPosition)).Equals(Vector3Int.FloorToInt(grid.CalculateGridPosition(previousPosition))) == false;
    }

    public static void PrepareStartAndEndPosition(Vector3 startPosition, Vector3 endPosition, ref Vector3Int minPoint, ref Vector3Int maxPoint, Vector3 mapBottomLeftCorner)
    {
        Vector3 startPositionForCalcultions = new Vector3(startPosition.x, 0, startPosition.z);
        Vector3 endPositionForCalculations = new Vector3(endPosition.x, 0, endPosition.z);
        if ((startPosition.z > endPosition.z && startPosition.x < endPosition.x) || (startPosition.z < endPosition.z && startPosition.x > endPosition.x))
        {
            startPositionForCalcultions = new Vector3(startPosition.x, 0, endPosition.z);
            endPositionForCalculations = new Vector3(endPosition.x, 0, startPosition.z);
        }
        var startPositionDistance = Mathf.Abs(Vector3.Distance(mapBottomLeftCorner, startPositionForCalcultions));
        var endPositionDistance = Mathf.Abs(Vector3.Distance(mapBottomLeftCorner, endPositionForCalculations));
        minPoint = Vector3Int.FloorToInt(startPositionDistance < endPositionDistance ? startPositionForCalcultions : endPositionForCalculations);
        maxPoint = Vector3Int.FloorToInt(startPositionDistance >= endPositionDistance ? startPositionForCalcultions : endPositionForCalculations);
    }

    public static void CalculateZone(HashSet<Vector3Int> newPositionsSet, Dictionary<Vector3Int, GameObject> structuresToBeModified, Queue<GameObject> gameObjectsToReuse)
    {
        HashSet<Vector3Int> existingStructuresPositions = new HashSet<Vector3Int>(structuresToBeModified.Keys);
        existingStructuresPositions.IntersectWith(newPositionsSet);
        HashSet<Vector3Int> structuresPositionsToDisable = new HashSet<Vector3Int>(structuresToBeModified.Keys);
        structuresPositionsToDisable.ExceptWith(newPositionsSet);

        foreach (var positionToDisable in structuresPositionsToDisable)
        {
            var structure = structuresToBeModified[positionToDisable];
            structure.SetActive(false);
            gameObjectsToReuse.Enqueue(structure);
            structuresToBeModified.Remove(positionToDisable);
        }

        foreach (var position in existingStructuresPositions)
        {
            newPositionsSet.Remove(position);
        }
    }
}
