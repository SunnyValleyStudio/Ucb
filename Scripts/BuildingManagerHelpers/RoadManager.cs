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
        throw new NotImplementedException();
    }

    internal static RoadStructureHelper CheckIfCornerFits(int neighboursStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        throw new NotImplementedException();
    }

    internal static RoadStructureHelper CheckIf3WayFits(int neighboursStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        throw new NotImplementedException();
    }

    internal static RoadStructureHelper CheckIf4WaysFits(int neighboursStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        throw new NotImplementedException();
    }
}
