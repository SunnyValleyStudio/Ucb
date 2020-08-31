using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlacementModificationHelper : StructureModificationHelper
{
    Dictionary<Vector3Int, GameObject> existingRoadStructuresToModify = new Dictionary<Vector3Int, GameObject>();
    public RoadPlacementModificationHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager) : base(structureRepository, grid, placementManager)
    {
    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        base.PrepareStructureForModification(inputPosition, structureName, structureType);
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (grid.IsCellTaken(gridPosition) == false)
        {
            var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
            var roadStructure = GetCorrectRoadPrefab(gridPosition);
            if (structuresToBeModified.ContainsKey(gridPositionInt))
            {
                RevokeRoadPlacementAt(gridPositionInt);
            }
            else
            {
                PlaceNewRoadAt(roadStructure, gridPosition, gridPositionInt);
            }
            AdjustNeighboursIfAreRoadStructures(gridPosition);
        }
    }

    private void AdjustNeighboursIfAreRoadStructures(Vector3 gridPositon)
    {
        AdjustNeighboursIfRoad(gridPositon, Direction.Up);
        AdjustNeighboursIfRoad(gridPositon, Direction.Down);
        AdjustNeighboursIfRoad(gridPositon, Direction.Right);
        AdjustNeighboursIfRoad(gridPositon, Direction.Left);
    }

    private void AdjustNeighboursIfRoad(Vector3 gridPositon, Direction direction)
    {
        var neighbourGridPosition = grid.GetPositionOfTheNeighbourIfExists(gridPositon, direction);
        if (neighbourGridPosition.HasValue)
        {
            var neighbourPositionInt = neighbourGridPosition.Value;
            AdjustStructureIfIsInDictionary(neighbourGridPosition, neighbourPositionInt);
            AdjustStructureIfIsOnGrid(neighbourGridPosition, neighbourPositionInt);

        }
    }

    private void AdjustStructureIfIsOnGrid(Vector3Int? neighbourGridPosition, Vector3Int neighbourPositionInt)
    {
        if (RoadManager.CheckIfNeighbourIsRoadOnTheGrid(grid, neighbourPositionInt))
        {
            var neighbourStructureData = grid.GetStructureDataFromTheGrid(neighbourGridPosition.Value);
            if (neighbourStructureData != null && neighbourStructureData.GetType() == typeof(RoadStructureSO) && existingRoadStructuresToModify.ContainsKey(neighbourPositionInt) == false)
            {
                existingRoadStructuresToModify.Add(neighbourPositionInt, grid.GetStructureFromTheGrid(neighbourGridPosition.Value));
            }
        }
    }

    private void AdjustStructureIfIsInDictionary(Vector3Int? neighbourGridPosition, Vector3Int neighbourPositionInt)
    {
        if (RoadManager.CheckIfNeighbourIsRoadInDictionary(neighbourPositionInt, structuresToBeModified))
        {
            RevokeRoadPlacementAt(neighbourPositionInt);
            var neighboursStructure = GetCorrectRoadPrefab(neighbourGridPosition.Value);
            PlaceNewRoadAt(neighboursStructure, neighbourGridPosition.Value, neighbourPositionInt);
        }
    }

    private void PlaceNewRoadAt(RoadStructureHelper roadStructure, Vector3 gridPosition, Vector3Int gridPositionInt)
    {
        structuresToBeModified.Add(gridPositionInt, placementManager.CreateGhostStructure(gridPosition, roadStructure.RoadPrefab, roadStructure.RoadPrefabRotation));
    }

    private void RevokeRoadPlacementAt(Vector3Int gridPositionInt)
    {
        var structure = structuresToBeModified[gridPositionInt];
        placementManager.DestroySingleStructure(structure);
        structuresToBeModified.Remove(gridPositionInt);
    }

    private RoadStructureHelper GetCorrectRoadPrefab(Vector3 gridPosition)
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


    public override void CancleModifications()
    {
        base.CancleModifications();
        existingRoadStructuresToModify.Clear();
    }

    public override void ConfirmModifications()
    {
        foreach (var keyValuePair in existingRoadStructuresToModify)
        {
            grid.RemoveStructureFromTheGrid(keyValuePair.Key);
            placementManager.DestroySingleStructure(keyValuePair.Value);
            var roadStructure = GetCorrectRoadPrefab(keyValuePair.Key);
            var structure = placementManager.PlaceStructureOnTheMap(keyValuePair.Key, roadStructure.RoadPrefab, roadStructure.RoadPrefabRotation);
            grid.PlaceStructureOnTheGrid(structure, keyValuePair.Key, structureData);
        }
        existingRoadStructuresToModify.Clear();
        base.ConfirmModifications();
    }



}
