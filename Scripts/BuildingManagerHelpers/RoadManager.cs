using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoadManager
{
    public static int GetRoadNeighboursStatus(Vector3 gridPosition, GridStructure grid, Dictionary<Vector3Int, GameObject> structuresToBeModified)
    {
        int roadNeighboursStatus = 0;
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var neighbourPosition = grid.GetPositionOfTheNeighbourIfExists(gridPosition, direction);
            if (neighbourPosition.HasValue)
            {
                if (CheckIfNeighbourIsRoadOnTheGrid(grid, neighbourPosition) || CheckIfNeighbourIsRoadInDictionary(neighbourPosition, structuresToBeModified))
                {
                    roadNeighboursStatus += (int)direction;
                }
            }
        }
        return roadNeighboursStatus;
    }

    public static bool CheckIfNeighbourIsRoadOnTheGrid(GridStructure grid, Vector3Int? neighbourPosition)
    {
        if (grid.IsCellTaken(neighbourPosition.Value))
        {
            var neighbourStructureData = grid.GetStructureDataFromTheGrid(neighbourPosition.Value);
            if (neighbourStructureData != null && neighbourStructureData.GetType() == typeof(RoadStructureSO))
            {
                return true;
            }
        }
        return false;
    }

    public static bool CheckIfNeighbourIsRoadInDictionary(Vector3Int? neighbourPosition, Dictionary<Vector3Int, GameObject> structuresToBeModified)
    {
        if (structuresToBeModified == null)
            return false;
        return structuresToBeModified.ContainsKey(neighbourPosition.Value);
    }

    public static RoadStructureHelper CheckIfStraightRoadFits(int neighboursStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
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

    public static RoadStructureHelper CheckIf3WayFits(int neighboursStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
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

    public static RoadStructureHelper CheckIf4WaysFits(int neighboursStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        if (neighboursStatus == ((int)Direction.Up | (int)Direction.Right | (int)Direction.Down | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).fourWayPrefab, RotationValue.R0);
        }
        return roadToReturn;
    }

    public static RoadStructureHelper CheckIfCornerFits(int neighboursStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
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

    public static Dictionary<Vector3Int, GameObject> GetRoadNeighboursForPosition(GridStructure grid, Vector3Int position)
    {
        Dictionary<Vector3Int, GameObject> dictionaryToReturn = new Dictionary<Vector3Int, GameObject>();
        List<Vector3Int?> neighbourPossibleLocations = new List<Vector3Int?>();
        neighbourPossibleLocations.Add(grid.GetPositionOfTheNeighbourIfExists(position, Direction.Up));
        neighbourPossibleLocations.Add(grid.GetPositionOfTheNeighbourIfExists(position, Direction.Down));
        neighbourPossibleLocations.Add(grid.GetPositionOfTheNeighbourIfExists(position, Direction.Left));
        neighbourPossibleLocations.Add(grid.GetPositionOfTheNeighbourIfExists(position, Direction.Right));
        foreach (var possiblePosition in neighbourPossibleLocations)
        {
            if (possiblePosition.HasValue)
            {
                if (CheckIfNeighbourIsRoadOnTheGrid(grid, possiblePosition.Value)
                    && dictionaryToReturn.ContainsKey(possiblePosition.Value) == false)
                {
                    dictionaryToReturn.Add(possiblePosition.Value, grid.GetStructureFromTheGrid(possiblePosition.Value));
                }
            }
        }
        return dictionaryToReturn;
    }

    public static void ModifyRoadCellsOnTheGrid(Dictionary<Vector3Int, GameObject> neighboursDictionar, StructureBaseSO structureData, Dictionary<Vector3Int, GameObject> structuresToBeModified, GridStructure grid, IPlacementManager placementManager)
    {
        foreach (var keyValuePair in neighboursDictionar)
        {
            grid.RemoveStructureFromTheGrid(keyValuePair.Key);
            placementManager.DestroySingleStructure(keyValuePair.Value);
            var roadStructure = GetCorrectRoadPrefab(keyValuePair.Key, structureData, structuresToBeModified, grid);
            var structure = placementManager.PlaceStructureOnTheMap(keyValuePair.Key, roadStructure.RoadPrefab, roadStructure.RoadPrefabRotation);
            grid.PlaceStructureOnTheGrid(structure, keyValuePair.Key, GameObject.Instantiate(structureData));
        }
        neighboursDictionar.Clear();
    }

    public static RoadStructureHelper GetCorrectRoadPrefab(Vector3 gridPosition, StructureBaseSO structureData, Dictionary<Vector3Int, GameObject> structuresToBeModified, GridStructure grid)
    {
        var neighboursStatus = RoadManager.GetRoadNeighboursStatus(gridPosition, grid, structuresToBeModified);
        RoadStructureHelper roadToReturn = null;
        roadToReturn = RoadManager.CheckIfStraightRoadFits(neighboursStatus, roadToReturn, structureData);
        if (roadToReturn != null)
            return roadToReturn;
        roadToReturn = RoadManager.CheckIfCornerFits(neighboursStatus, roadToReturn, structureData);
        if (roadToReturn != null)
            return roadToReturn;
        roadToReturn = RoadManager.CheckIf3WayFits(neighboursStatus, roadToReturn, structureData);
        if (roadToReturn != null)
            return roadToReturn;
        roadToReturn = RoadManager.CheckIf4WaysFits(neighboursStatus, roadToReturn, structureData);
        return roadToReturn;
    }
}
