using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureModificationFactory
{
    private readonly StructureModificationHelper singleStructurePlacementHelper;
    private readonly StructureModificationHelper structureDemolitionHelper;
    public StructureModificationFactory(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager)
    {
        singleStructurePlacementHelper = new SingleStructurePlacementHelper(structureRepository, grid, placementManager);
        structureDemolitionHelper = new StructureDemolitionHelper(structureRepository, grid, placementManager);
    }

    public StructureModificationHelper GetHelper(Type classType)
    {
        if (classType == typeof(PlayerDemolitionState))
        {
            return structureDemolitionHelper;
        }
        else
        {
            return singleStructurePlacementHelper;
        }
    }
}
