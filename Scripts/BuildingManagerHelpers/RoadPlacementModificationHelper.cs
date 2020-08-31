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
        }
    }

    private void PlaceNewRoadAt(RoadStructureHelper roadStructure, Vector3 gridPosition, Vector3Int gridPositionInt)
    {
        throw new NotImplementedException();
    }

    private void RevokeRoadPlacementAt(Vector3Int gridPositionInt)
    {
        throw new NotImplementedException();
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

    

    
}
