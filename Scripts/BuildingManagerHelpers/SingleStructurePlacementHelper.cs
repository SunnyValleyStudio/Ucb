using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStructurePlacementHelper
{
    Dictionary<Vector3Int, GameObject> structuresToBeModified = new Dictionary<Vector3Int, GameObject>();
    private readonly StructureRepository structureRepository;
    private readonly GridStructure grid;
    private readonly PlacementManager placementManager;

    public SingleStructurePlacementHelper(StructureRepository structureRepository, GridStructure grid, PlacementManager placementManager)
    {
        this.structureRepository = structureRepository;
        this.grid = grid;
        this.placementManager = placementManager;
    }

    public void PrepareStructureForPlacement(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        GameObject buildingPrefab = this.structureRepository.GetBuildingPrefabByName(structureName, structureType);
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
        if (grid.IsCellTaken(gridPosition) == false)
        {
            if (structuresToBeModified.ContainsKey(gridPositionInt))
            {
                RevokeStructurePlacementAt(gridPositionInt);

            }
            else
            {
                PlaceNewStructureAt(buildingPrefab, gridPosition, gridPositionInt);
            }
        }
    }

    private void PlaceNewStructureAt(GameObject buildingPrefab, Vector3 gridPosition, Vector3Int gridPositionInt)
    {
        structuresToBeModified.Add(gridPositionInt, placementManager.CreateGhostStructure(gridPosition, buildingPrefab));
    }

    private void RevokeStructurePlacementAt(Vector3Int gridPositionInt)
    {
        var structure = structuresToBeModified[gridPositionInt];
        placementManager.DestroySingleStructure(structure);
        structuresToBeModified.Remove(gridPositionInt);
    }

    public void ConfirmPlacement()
    {
        placementManager.PlaceStructuresOnTheMap(structuresToBeModified.Values);
        foreach (var keyValuePair in structuresToBeModified)
        {
            grid.PlaceStructureOnTheGrid(keyValuePair.Value, keyValuePair.Key);
        }
        structuresToBeModified.Clear();
    }

    public void CanclePlacement()
    {
        placementManager.DestroyStructures(structuresToBeModified.Values);
        structuresToBeModified.Clear();
    }

    public GameObject AccessStructureInDictionary(Vector3 gridPosition)
    {
        var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
        if (structuresToBeModified.ContainsKey(gridPositionInt))
        {
            return structuresToBeModified[gridPositionInt];
        }
        return null;
    }

    public GameObject CheckForStructureToModifyDictionary(Vector3 inputPostion)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPostion);
        var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
        if (structuresToBeModified.ContainsKey(gridPositionInt))
        {
            return structuresToBeModified[gridPositionInt];
        }
        return null;
    }
}
