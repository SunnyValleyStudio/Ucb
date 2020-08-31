using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureDemolitionHelper : StructureModificationHelper
{
    public StructureDemolitionHelper(StructureRepository structureRepository, GridStructure grid, PlacementManager placementManager) : base(structureRepository, grid, placementManager)
    {
    }
    public override void CancleModifications()
    {
        this.placementManager.PlaceStructuresOnTheMap(structuresToBeModified.Values);
        structuresToBeModified.Clear();
    }

    public override void ConfirmModifications()
    {
        foreach (var gridPosition in structuresToBeModified.Keys)
        {
            grid.RemoveStructureFromTheGrid(gridPosition);
        }
        this.placementManager.DestroyStructures(structuresToBeModified.Values);
        structuresToBeModified.Clear();
    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
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

}
