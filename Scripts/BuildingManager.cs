using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    IPlacementManager placementManager;
    StructureRepository structureRepository;
    StructureModificationHelper helper;

    public BuildingManager(int cellSize, int width, int length, IPlacementManager placementManager, StructureRepository structureRepository, IResourceManager resourceManager)
    {
        this.grid = new GridStructure(cellSize, width, length);
        this.placementManager = placementManager;
        this.structureRepository = structureRepository;
        StructureModificationFactory.PrepareFactory(structureRepository, grid, placementManager, resourceManager);

    }

    public void PrepareBuildingManager(Type classType)
    {
        helper = StructureModificationFactory.GetHelper(classType);
    }

    public void PrepareStructureForPlacement(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        helper.PrepareStructureForModification(inputPosition, structureName, structureType);
    }

    public void ConfirmModification()
    {
        helper.ConfirmModifications();
    }

    public IEnumerable<StructureBaseSO> GetAllStructures()
    {
        return grid.GetAllStructures();
    }

    public void CancelModification()
    {
        helper.CancleModifications();
    }

    public void PrepareStructureForDemolitionAt(Vector3 inputPosition)
    {
        helper.PrepareStructureForModification(inputPosition, "", StructureType.None);
    }

    public GameObject CheckForStructureInGrid(Vector3 inputPosition)
    {
        Vector3 gridPositoion = grid.CalculateGridPosition(inputPosition);
        if (grid.IsCellTaken(gridPositoion))
        {
            return grid.GetStructureFromTheGrid(gridPositoion);
        }
        return null;
    }

    public GameObject CheckForStructureInDictionary(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        GameObject structureToReturn = null;
        structureToReturn = helper.AccessStructureInDictionary(gridPosition);
        if (structureToReturn != null)
        {
            return structureToReturn;
        }
        structureToReturn = helper.AccessStructureInDictionary(gridPosition);
        return structureToReturn;
    }

    public void StopContinuousPlacement()
    {
        helper.StopContinuousPlacement();
    }

    public StructureBaseSO GetStructureDataFromPosition(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (grid.IsCellTaken(gridPosition))
        {
            return grid.GetStructureDataFromTheGrid(inputPosition);

        }
        return null;
    }
}
