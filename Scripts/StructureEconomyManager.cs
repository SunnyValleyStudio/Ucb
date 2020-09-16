using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureEconomyManager : MonoBehaviour
{
    private static void PrepareNewStructure(Vector3Int gridPosition, GridStructure grid)
    {
        var structureData = grid.GetStructureDataFromTheGrid(gridPosition);
        var structuresAroundZoneStructure = grid.GetStructuresDataInRange(gridPosition, structureData.structureRange);
        //adds roads
        structureData.PreareStructure(structuresAroundZoneStructure);
    }

    //what to do when zone structure is placed
    public static void PrepareZoneStructure(Vector3Int gridPosition, GridStructure grid)
    {
        PrepareNewStructure(gridPosition, grid);
        ZoneStructureSO zoneData = (ZoneStructureSO)grid.GetStructureDataFromTheGrid(gridPosition);
        if (DoesStructureRequireAnyResource(zoneData))
        {
            var structuresAroundPositions = grid.GetStructurePositionsInRange(gridPosition, zoneData.maxFacilitySearchRange);
            foreach (var structurePositionNearby in structuresAroundPositions)
            {
                var data = grid.GetStructureDataFromTheGrid(structurePositionNearby);
                if (data.GetType() == typeof(SingleFacilitySO))
                {
                    SingleFacilitySO facility = (SingleFacilitySO)data;
                    if ((facility.facilityType == FacilityType.Power && zoneData.HasPower() == false && zoneData.requirePower)
                        || (facility.facilityType == FacilityType.Water && zoneData.HasWater() == false && zoneData.requireWater))
                    {
                        if (grid.ArePositionsInRange(gridPosition, structurePositionNearby, facility.singleStructureRange))
                        {
                            if (facility.IsFull() == false)
                            {
                                facility.AddClients(new StructureBaseSO[] { zoneData });
                                if (DoesStructureRequireAnyResource(zoneData) == false)
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private static bool DoesStructureRequireAnyResource(ZoneStructureSO zoneData)
    {
        return (zoneData.requirePower && zoneData.HasPower() == false) || (zoneData.requireWater && zoneData.HasWater() == false);
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
        PrepareNewStructure(gridPosition, grid);

        SingleFacilitySO faciltityData = (SingleFacilitySO)grid.GetStructureDataFromTheGrid(gridPosition);
        var structuresAroundFacility = grid.GetStructuresDataInRange(gridPosition, faciltityData.singleStructureRange);
        faciltityData.AddClients(structuresAroundFacility);
    }

    public static IEnumerable<StructureBaseSO> PrepareFacilityDemolition(Vector3Int gridPosition, GridStructure grid)
    {
        SingleFacilitySO faciltityData = (SingleFacilitySO)grid.GetStructureDataFromTheGrid(gridPosition);
        return faciltityData.PrepareForDestruction();
    }

    public static IEnumerable<StructureBaseSO> PrepareRoadDemolition(Vector3Int gridPosition, GridStructure grid)
    {
        RoadStructureSO roadData = (RoadStructureSO)grid.GetStructureDataFromTheGrid(gridPosition);
        var structuresAroundRoad = grid.GetStructuresDataInRange(gridPosition, roadData.structureRange);
        return roadData.PrepareRoadDemolition(structuresAroundRoad);
    }

    public static void PrepareStructureForDemolition(Vector3Int gridPosition, GridStructure grid)
    {
        var structureData = grid.GetStructureDataFromTheGrid(gridPosition);
        structureData.PrepareForDestruction();
    }

    public static void CreateStructureLogic(Type structureType, Vector3Int gridPosition, GridStructure grid)
    {
        if (structureType == typeof(ZoneStructureSO))
        {
            PrepareZoneStructure(gridPosition, grid);
        }
        else if (structureType == typeof(RoadStructureSO))
        {
            PrepareRoadStructure(gridPosition, grid);
        }
        else if (structureType == typeof(SingleFacilitySO))
        {
            PrepareFacilityStructure(gridPosition, grid);
        }
    }

    public static void DemolitionStructureLogic(Type structureType, Vector3Int gridPosition, GridStructure grid)
    {
        if (structureType == typeof(ZoneStructureSO))
        {
            PrepareStructureForDemolition(gridPosition, grid);
        }
        else if (structureType == typeof(RoadStructureSO))
        {
            PrepareRoadDemolition(gridPosition, grid);
        }
        else if (structureType == typeof(SingleFacilitySO))
        {
            PrepareFacilityDemolition(gridPosition, grid);
        }
    }
}
