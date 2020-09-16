using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureEconomyManager : MonoBehaviour
{
    private static void PrepareNewStructure(Vector3Int gridPosition, GridStructure grid)
    {
        var zoneData = grid.GetStructureDataFromTheGrid(gridPosition);
        var structuresAroundZoneStructure = grid.GetStructuresDataInRange(gridPosition, zoneData.structureRange);
        //adds roads
        zoneData.PreareStructure(structuresAroundZoneStructure);
    }

    //what to do when zone structure is placed
    public static void PrepareZoneStructure(Vector3Int gridPosition, GridStructure grid)
    {
        PrepareNewStructure(gridPosition, grid);

    }

    //what to do when road is placed
    public static void PrepareRoadStructure(Vector3Int gridPosition, GridStructure grid)
    {
        RoadStructureSO roadData = (RoadStructureSO)grid.GetStructureDataFromTheGrid(gridPosition);
        var structuresAroundRoad = grid.GetStructuresDataInRange(gridPosition, roadData.structureRange);
        roadData.PrepareRoad(structuresAroundRoad);

    }

    public static void PrepareFacilityStructure(Vector3Int gridPosition, GridStructure grid)
    {
        PrepareZoneStructure(gridPosition, grid);

        SingleFacilitySO faciltityData = (SingleFacilitySO)grid.GetStructureDataFromTheGrid(gridPosition);
        var structuresAroundFacility = grid.GetStructuresDataInRange(gridPosition, faciltityData.singleStructureRange);
        faciltityData.AddClients(structuresAroundFacility);
    }

    public static IEnumerable<StructureBaseSO> PrepareFacilityDemolition(Vector3Int gridPosition, GridStructure grid)
    {
        SingleFacilitySO faciltityData = (SingleFacilitySO)grid.GetStructureDataFromTheGrid(gridPosition);
        return faciltityData.PrepareFacilityDestruction();
    }

    public static void PrepareStructureForDemolition(Vector3Int gridPosition, GridStructure grid)
    {
        var structureData = grid.GetStructureDataFromTheGrid(gridPosition);
        structureData.PrepareStructureForDemolition();
    }
    //what to do when single structure is placed

    //what to do when structure is deleted
}
