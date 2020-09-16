using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureModificationHelper
{
    protected Dictionary<Vector3Int, GameObject> structuresToBeModified = new Dictionary<Vector3Int, GameObject>();
    protected readonly StructureRepository structureRepository;
    protected readonly GridStructure grid;
    protected readonly IPlacementManager placementManager;
    protected StructureBaseSO structureData;
    protected IResourceManager resourceManager;

    public StructureModificationHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager, IResourceManager resourceManager)
    {
        this.structureRepository = structureRepository;
        this.grid = grid;
        this.placementManager = placementManager;
        this.resourceManager = resourceManager;
        structureData = ScriptableObject.CreateInstance<NullStructureSO>();
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

    public virtual void ConfirmModifications()
    {
        placementManager.PlaceStructuresOnTheMap(structuresToBeModified.Values);
        Type structureType = structureData.GetType();
        foreach (var keyValuePair in structuresToBeModified)
        {
            grid.PlaceStructureOnTheGrid(keyValuePair.Value, keyValuePair.Key, GameObject.Instantiate(structureData) );
            StructureEconomyManager.CreateStructureLogic(structureType, keyValuePair.Key, grid);
        }
        ResetHelpersData();
    }

    public virtual void CancleModifications()
    {
        placementManager.DestroyStructures(structuresToBeModified.Values);
        ResetHelpersData();
    }
    public virtual void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        if (structureData.GetType()==typeof(NullStructureSO) && structureType != StructureType.None)
        {
            structureData = this.structureRepository.GetStructureData(structureName, structureType);
        }
    }

    protected void ResetHelpersData()
    {
        structureData = ScriptableObject.CreateInstance<NullStructureSO>();
        structuresToBeModified.Clear();
    }

    public virtual void StopContinuousPlacement()
    {

    }
}
