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
        

    }


}
