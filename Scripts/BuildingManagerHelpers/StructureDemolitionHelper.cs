using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureDemolitionHelper
{
    Dictionary<Vector3Int, GameObject> structuresToBeModified = new Dictionary<Vector3Int, GameObject>();
    private readonly StructureRepository structureRepository;
    private readonly GridStructure grid;
    private readonly PlacementManager placementManager;

    public StructureDemolitionHelper(StructureRepository structureRepository, GridStructure grid, PlacementManager placementManager)
    {
        this.structureRepository = structureRepository;
        this.grid = grid;
        this.placementManager = placementManager;
    }

    public void PrepareStructureForDemolitionAt(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (grid.IsCellTaken(gridPosition))
        {
            var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
            var structure = grid.GetStructureFromTheGrid(gridPosition);
            if (structuresToBeModified.ContainsKey(gridPositionInt))
            {
                RevokeStructureDemolitionAt(gridPositionInt, structure);
            }
            else
            {
                AddStructureForDemolition(gridPositionInt, structure);
            }
        }
    }

    private void AddStructureForDemolition(Vector3Int gridPositionInt, GameObject structure)
    {
        structuresToBeModified.Add(gridPositionInt, structure);
        placementManager.SetBuildingForDemolition(structure);
    }

    private void RevokeStructureDemolitionAt(Vector3Int gridPositionInt, GameObject structure)
    {
        placementManager.ResetBuildingMaterial(structure);
        structuresToBeModified.Remove(gridPositionInt);
    }

    public void CancleDemolition()
    {
        this.placementManager.PlaceStructuresOnTheMap(structuresToBeModified.Values);
        structuresToBeModified.Clear();
    }

    public void ConfirmDemolition()
    {
        foreach (var gridPosition in structuresToBeModified.Keys)
        {
            grid.RemoveStructureFromTheGrid(gridPosition);
        }
        this.placementManager.DestroyStructures(structuresToBeModified.Values);
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
}
