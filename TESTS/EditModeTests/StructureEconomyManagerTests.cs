using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class StructureEconomyManagerTests
    {
        GridStructure grid;
        GameObject structureObject = new GameObject();

        [SetUp]
        public void Init()
        {
            grid = new GridStructure(3, 10, 10);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void StructureEconomyManagerAddReidentialStructureNoRoad()
        {
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));
            Assert.False(residentialZone.HasRoadAccess());
        }

        [Test]
        public void StructureEconomyManagerAddReidentialStructureNearRoadConnection()
        {
            CreateRoadATPosition(new Vector3Int(3, 0, 0));
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));
            Assert.True(residentialZone.HasRoadAccess());
        }

        [Test]
        public void StructureEconomyManagerAddReidentialStructureNearRoadDiagonalNoConnection()
        {

            CreateRoadATPosition(new Vector3Int(3, 0, 3));
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));

            Assert.False(residentialZone.HasRoadAccess());
        }


        [Test]
        public void StructureEconomyManagerAddRoadNearReidentialStructureConnection()
        {
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));
            CreateRoadATPosition(new Vector3Int(3, 0, 0));
            Assert.True(residentialZone.HasRoadAccess());
        }

        [Test]
        public void StructureEconomyManagerAddRoadNearReidentialStructureDiagonalNoConnection()
        {
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));
            CreateRoadATPosition(new Vector3Int(3, 0, 3));
            Assert.False(residentialZone.HasRoadAccess());
        }

        [Test]
        public void StructureEconomyManagerRemoveRoadNearReidentialStructureConnection()
        {
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));
            RoadStructureSO roadStructure = ScriptableObject.CreateInstance<RoadStructureSO>();
            CreateRoadATPosition(new Vector3Int(3, 0, 0));
            StructureEconomyManager.PrepareRoadDemolition(new Vector3Int(3, 0, 0), grid);
            grid.RemoveStructureFromTheGrid(new Vector3(3, 0, 0));
            Assert.False(residentialZone.HasRoadAccess());
        }

        [Test]
        public void StructureEconomyManagerRemoveRoadNearReidentialStructureDiagonalNoConnection()
        {
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));
            CreateRoadATPosition(new Vector3Int(3, 0, 3));

            StructureEconomyManager.PrepareRoadDemolition(new Vector3Int(3, 0, 3), grid);
            grid.RemoveStructureFromTheGrid(new Vector3(3, 0, 3));
            Assert.False(residentialZone.HasRoadAccess());
        }


        [Test]
        public void StructureEconomyManagerAddRoadNear3ReidentialStructureConnectionWith1()
        {
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));
            ZoneStructureSO residentialZone1 = CreateZOneAtPosition(new Vector3Int(0, 0, 3));
            ZoneStructureSO residentialZone2 = CreateZOneAtPosition(new Vector3Int(0, 0, 6));
            CreateRoadATPosition(new Vector3Int(3, 0, 3));

            Assert.False(residentialZone.HasRoadAccess());
            Assert.True(residentialZone1.HasRoadAccess());
            Assert.False(residentialZone2.HasRoadAccess());
        }

        [Test]
        public void StructureEconomyManagerAdd3ReidentialStructureNearRoadConnectionWith1()
        {
            CreateRoadATPosition(new Vector3Int(3, 0, 3));
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));
            ZoneStructureSO residentialZone1 = CreateZOneAtPosition(new Vector3Int(0, 0, 3));
            ZoneStructureSO residentialZone2 = CreateZOneAtPosition(new Vector3Int(0, 0, 6));


            Assert.False(residentialZone.HasRoadAccess());
            Assert.True(residentialZone1.HasRoadAccess());
            Assert.False(residentialZone2.HasRoadAccess());
        }

        [Test]
        public void StructureEconomyManagerAdd3FacilityStructureNearRoadConnectionWith1()
        {
            CreateRoadATPosition(new Vector3Int(3, 0, 3));
            SingleFacilitySO facility = CreateFacilityAtPosition(new Vector3Int(0, 0, 0));
            SingleFacilitySO facility1 = CreateFacilityAtPosition(new Vector3Int(0, 0, 3));
            SingleFacilitySO facility2 = CreateFacilityAtPosition(new Vector3Int(0, 0, 6));


            Assert.False(facility.HasRoadAccess());
            Assert.True(facility1.HasRoadAccess());
            Assert.False(facility2.HasRoadAccess());
        }

        [Test]
        public void StructureEconomyManagerAddPowerFacilityNear3Residential3Connected()
        {
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));
            ZoneStructureSO residentialZone1 = CreateZOneAtPosition(new Vector3Int(0, 0, 3));
            ZoneStructureSO residentialZone2 = CreateZOneAtPosition(new Vector3Int(0, 0, 6));
            SingleFacilitySO facility = CreateFacilityAtPosition(new Vector3Int(6, 0, 6), FacilityType.Power);

            Assert.True(residentialZone.HasPower());
            Assert.True(residentialZone1.HasPower());
            Assert.True(residentialZone2.HasPower());
            Assert.True(facility.GetNumberOfCustomers() == 3);
        }

        [Test]
        public void StructureEconomyManagerRemovePowerFacilityNear3Residential3Connected()
        {
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));
            ZoneStructureSO residentialZone1 = CreateZOneAtPosition(new Vector3Int(0, 0, 3));
            ZoneStructureSO residentialZone2 = CreateZOneAtPosition(new Vector3Int(0, 0, 6));
            SingleFacilitySO facility = CreateFacilityAtPosition(new Vector3Int(6, 0, 6), FacilityType.Power);
            StructureEconomyManager.PrepareFacilityDemolition(new Vector3Int(6, 0, 6), grid);
            grid.RemoveStructureFromTheGrid(new Vector3Int(6, 0, 6));
            Assert.False(residentialZone.HasPower());
            Assert.False(residentialZone1.HasPower());
            Assert.False(residentialZone2.HasPower());
            Assert.True(grid.GetStructureDataFromTheGrid(new Vector3Int(6, 0, 6)) == null);
        }

        [Test]
        public void StructureEconomyManager3ResidentialsConnectedToFacilityRemove2()
        {
            ZoneStructureSO residentialZone = CreateZOneAtPosition(new Vector3Int(0, 0, 0));
            ZoneStructureSO residentialZone1 = CreateZOneAtPosition(new Vector3Int(0, 0, 3));
            ZoneStructureSO residentialZone2 = CreateZOneAtPosition(new Vector3Int(0, 0, 6));
            SingleFacilitySO facility = CreateFacilityAtPosition(new Vector3Int(6, 0, 6), FacilityType.Power);
            StructureEconomyManager.PrepareStructureForDemolition(new Vector3Int(0, 0, 0), grid);
            StructureEconomyManager.PrepareStructureForDemolition(new Vector3Int(0, 0, 3), grid);
            grid.RemoveStructureFromTheGrid(new Vector3(0, 0, 0));
            grid.RemoveStructureFromTheGrid(new Vector3(0, 0, 3));
            Assert.True(residentialZone2.HasPower());
            Assert.True(facility.GetNumberOfCustomers() == 1);
        }

        private SingleFacilitySO CreateFacilityAtPosition(Vector3Int positon, FacilityType facilityType = FacilityType.None)
        {
            SingleFacilitySO facility = new SingleFacilitySO();
            facility.requireRoadAccess = true;
            facility.singleStructureRange = 3;
            facility.facilityType = facilityType;
            facility.maxCustomers = 3;
            grid.PlaceStructureOnTheGrid(structureObject, positon, facility);
            StructureEconomyManager.PrepareFacilityStructure(positon, grid);
            return facility;
        }

        private void CreateRoadATPosition(Vector3Int positon)
        {
            RoadStructureSO roadStructure = ScriptableObject.CreateInstance<RoadStructureSO>();
            grid.PlaceStructureOnTheGrid(structureObject, positon, roadStructure);
            StructureEconomyManager.PrepareRoadStructure(positon, grid);
        }

        private ZoneStructureSO CreateZOneAtPosition(Vector3Int positon)
        {
            ZoneStructureSO residentialZone = CreateResidentialZone();
            grid.PlaceStructureOnTheGrid(structureObject, positon, residentialZone);
            StructureEconomyManager.PrepareZoneStructure(positon, grid);
            return residentialZone;
        }

        private static ZoneStructureSO CreateResidentialZone()
        {
            ZoneStructureSO residentialZone = ScriptableObject.CreateInstance<ZoneStructureSO>();
            residentialZone.requireRoadAccess = true;
            residentialZone.requirePower = true;
            residentialZone.requireWater = true;
            residentialZone.upkeepCost = 30;
            residentialZone.maxFacilitySearchRange = 2;
            return residentialZone;
        }
    }
}
