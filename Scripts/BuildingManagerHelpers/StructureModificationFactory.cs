using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureModificationFactory
{
    private readonly StructureModificationHelper singleStructurePlacementHelper;
    private readonly StructureModificationHelper structureDemolitionHelper;
    private readonly RoadPlacementModificationHelper roadStructurePlacementHelper;
    public StructureModificationFactory(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager)
    {
        singleStructurePlacementHelper = new SingleStructurePlacementHelper(structureRepository, grid, placementManager);
        structureDemolitionHelper = new StructureDemolitionHelper(structureRepository, grid, placementManager);
        roadStructurePlacementHelper = new RoadPlacementModificationHelper(structureRepository, grid, placementManager);
    }

    public StructureModificationHelper GetHelper(Type classType)
    {
        if (classType == typeof(PlayerDemolitionState))
        {
            return structureDemolitionHelper;
        }
        else if (classType == typeof(PlayerBuildingRoadState))
        {
            return roadStructurePlacementHelper;
        }
        else
        {
            return singleStructurePlacementHelper;
        }
    }
}
