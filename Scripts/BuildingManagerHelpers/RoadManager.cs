using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoadManager
{
    public static int GetRoadNeighboursStatus(Vector3 gridPosition, GridStructure grid, Dictionary<Vector3Int, GameObject> structuresToBeModified)
    {
        var roadNeighboursStatus = 0;

        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var neighbourPosition = grid.GetPositionOfTheNeighbourIfExists(gridPosition, direction);
            if (neighbourPosition.HasValue && grid.IsCellTaken(neighbourPosition.Value))
            {

                var neighbourStructureData = grid.GetStructureDataFromTheGrid(neighbourPosition.Value);
                if (neighbourStructureData != null || CheckDictionaryForRoadAtNeighbour(neighbourPosition.Value, structuresToBeModified))
                {
                    roadNeighboursStatus += (int)direction;
                }
            }

        }
        return roadNeighboursStatus;
    }

    public static bool CheckDictionaryForRoadAtNeighbour(Vector3Int value, Dictionary<Vector3Int, GameObject> structuresToBeModified)
    {
        return structuresToBeModified.ContainsKey(value);
    }

    internal static RoadStructureHelper CheckIfStraightRoadFits(int neighboursStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        if (neighboursStatus == ((int)Direction.Up | (int)Direction.Down) || neighboursStatus == (int)Direction.Up || neighboursStatus == (int)Direction.Down)
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).prefab, RotationValue.R90);
        }
        else if (neighboursStatus == ((int)Direction.Right | (int)Direction.Left) || neighboursStatus == (int)Direction.Right
        || neighboursStatus == (int)Direction.Left || neighboursStatus == 0)
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).prefab, RotationValue.R0);
        }
        return roadToReturn;
    }

    internal static RoadStructureHelper CheckIf3WayFits(int neighboursStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        if (neighboursStatus == ((int)Direction.Up | (int)Direction.Right | (int)Direction.Down))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R0);
        }
        else if (neighboursStatus == ((int)Direction.Left | (int)Direction.Up | (int)Direction.Right))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R270);
        }
        else if (neighboursStatus == ((int)Direction.Down | (int)Direction.Left | (int)Direction.Up))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R180);
        }
        else if (neighboursStatus == ((int)Direction.Right | (int)Direction.Down | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R90);
        }
        return roadToReturn;
    }

    internal static RoadStructureHelper CheckIf4WaysFits(int neighboursStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        if (neighboursStatus == ((int)Direction.Up | (int)Direction.Right | (int)Direction.Down | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).fourWayPrefab, RotationValue.R0);
        }
        return roadToReturn;
    }

    internal static RoadStructureHelper CheckIfCornerFits(int neighboursStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        if (neighboursStatus == ((int)Direction.Up | (int)Direction.Right))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R0);
        }
        else if (neighboursStatus == ((int)Direction.Down | (int)Direction.Right))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R90);
        }
        else if (neighboursStatus == ((int)Direction.Down | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R180);
        }
        else if (neighboursStatus == ((int)Direction.Up | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R270);
        }
        return roadToReturn;
    }
}
