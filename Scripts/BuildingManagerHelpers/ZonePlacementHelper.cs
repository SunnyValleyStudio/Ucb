using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePlacementHelper : StructureModificationHelper
{
    Vector3 startPosition;
    Vector3? previousEndPositon = null;
    bool startPositionAcquired = false;
    Vector3 mapBottomLeftCorner;
    Queue<GameObject> gameObjectsToReuse = new Queue<GameObject>();
    public ZonePlacementHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager, Vector3 mapBottomLeftCorner) : base(structureRepository, grid, placementManager)
    {
        this.mapBottomLeftCorner = mapBottomLeftCorner;
    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        base.PrepareStructureForModification(inputPosition, structureName, structureType);
        Vector3 gridPositon = grid.CalculateGridPosition(inputPosition);
        if (startPositionAcquired == false && grid.IsCellTaken(gridPositon) == false)
        {
            startPosition = gridPositon;
            startPositionAcquired = true;
        }
        if (startPositionAcquired && previousEndPositon == null || ZoneCalculator.CheckIfPositionHasChanged(gridPositon, previousEndPositon.Value, grid))
        {
            PlaceNewZoneUpTo(gridPositon);
        }

    }

    private void PlaceNewZoneUpTo(Vector3 endPosition)
    {
        Vector3Int minPoint = Vector3Int.FloorToInt(startPosition);
        Vector3Int maxPoint = Vector3Int.FloorToInt(endPosition);

        ZoneCalculator.PrepareStartAndEndPosition(startPosition, endPosition, ref minPoint, ref maxPoint, mapBottomLeftCorner);
        HashSet<Vector3Int> newPositionsSet = grid.GetAllPositionsFromTo(minPoint, maxPoint);
        previousEndPositon = endPosition;
        ZoneCalculator.CalculateZone(newPositionsSet, structuresToBeModified, gameObjectsToReuse);

        foreach (var positionToPlaceStructure in newPositionsSet)
        {
            if (grid.IsCellTaken(positionToPlaceStructure))
                continue;
            GameObject structureToAdd = null;
            if (gameObjectsToReuse.Count > 0)
            {
                var gameObjectToReuse = gameObjectsToReuse.Dequeue();
                gameObjectToReuse.SetActive(true);
                structureToAdd = placementManager.MoveStructureOnTheMap(positionToPlaceStructure, gameObjectToReuse, structureData.prefab);

            }
            else
            {
                structureToAdd = placementManager.CreateGhostStructure(positionToPlaceStructure, structureData.prefab);

            }
            structuresToBeModified.Add(positionToPlaceStructure, structureToAdd);
        }


    }

    public override void CancleModifications()
    {
        base.CancleModifications();
        ResetZonePlacementHelper();
    }

    public override void ConfirmModifications()
    {
        base.ConfirmModifications();
        ResetZonePlacementHelper();
    }

    private void ResetZonePlacementHelper()
    {
        placementManager.DestroyStructures(gameObjectsToReuse);
        gameObjectsToReuse.Clear();
        startPositionAcquired = false;
        previousEndPositon = null;
    }

    public override void StopContinuousPlacement()
    {
        startPositionAcquired = false;
        base.StopContinuousPlacement();
    }

}
