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

    public StructureModificationHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager)
    {
        this.structureRepository = structureRepository;
        this.grid = grid;
        this.placementManager = placementManager;
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
        foreach (var keyValuePair in structuresToBeModified)
        {
            grid.PlaceStructureOnTheGrid(keyValuePair.Value, keyValuePair.Key, GameObject.Instantiate(structureData) );
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
        if (structureData == null && structureType != StructureType.None)
        {
            structureData = this.structureRepository.GetStructureData(structureName, structureType);
        }
    }

    protected void ResetHelpersData()
    {
        structureData = null;
        structuresToBeModified.Clear();
    }

    public virtual void StopContinuousPlacement()
    {

    }
}
