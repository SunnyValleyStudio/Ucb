using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StructureModificationFactory
{
    private static StructureModificationHelper singleStructurePlacementHelper;
    private static StructureModificationHelper structureDemolitionHelper;
    private static StructureModificationHelper roadStructurePlacementHelper;
    private static StructureModificationHelper zonePlacementHelper;
    public static void PrepareFactory(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManager, IResourceManager resourceManager)
    {
        singleStructurePlacementHelper = new SingleStructurePlacementHelper(structureRepository, grid, placementManager, resourceManager);
        structureDemolitionHelper = new StructureDemolitionHelper(structureRepository, grid, placementManager, resourceManager);
        roadStructurePlacementHelper = new RoadPlacementModificationHelper(structureRepository, grid, placementManager, resourceManager);
        zonePlacementHelper = new ZonePlacementHelper(structureRepository, grid, placementManager, Vector3.zero, resourceManager);
    }

    public static StructureModificationHelper GetHelper(Type classType)
    {
        if (classType == typeof(PlayerDemolitionState))
        {
            return structureDemolitionHelper;
        }
        else if (classType == typeof(PlayerBuildingZoneState))
        {
            return zonePlacementHelper;
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
