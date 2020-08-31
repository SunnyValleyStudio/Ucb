using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StructureModificationFactory
{
    private static StructureModificationHelper singleStructurePlacementHelper;
    private static StructureModificationHelper structureDemolitionHelper;
    private static RoadPlacementModificationHelper roadStructurePlacementHelper;
    public static void PrepareFactory(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager)
    {
        singleStructurePlacementHelper = new SingleStructurePlacementHelper(structureRepository, grid, placementManager);
        structureDemolitionHelper = new StructureDemolitionHelper(structureRepository, grid, placementManager);
        roadStructurePlacementHelper = new RoadPlacementModificationHelper(structureRepository, grid, placementManager);
    }

    public static StructureModificationHelper GetHelper(Type classType)
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
