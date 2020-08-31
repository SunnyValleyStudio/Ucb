using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class StructureDemolitionHelperTest
    {
        Vector3 gridPosition1 = Vector3.zero;
        Vector3 gridPosition2 = new Vector3(3, 0, 3);
        GameObject tempObject = null;
        StructureModificationHelper helper;
        string structureName = "Road";
        StructureType structureType = StructureType.Road;
        GridStructure grid;

        [SetUp]
        public void Init()
        {
            StructureRepository structureRepository = TestHelpers.CreateStructureRepositoryContainingRoad();
            IPlacementManager placementManager = Substitute.For<IPlacementManager>();
            
            tempObject = new GameObject();
            placementManager.CreateGhostStructure(default, default).ReturnsForAnyArgs(tempObject);
            grid = new GridStructure(3, 10, 10);

            grid.PlaceStructureOnTheGrid(tempObject, gridPosition1, null);
            grid.PlaceStructureOnTheGrid(tempObject, gridPosition2, null);

            IResourceManager resourceManager = Substitute.For<IResourceManager>();
            resourceManager.CanIBuyIt(default).Returns(true);

            helper = new StructureDemolitionHelper(structureRepository, grid, placementManager, resourceManager);

        }
        [Test]
        public void StructureDemolitionHelperSelectForDemolitionPasses()
        {
            helper.PrepareStructureForModification(gridPosition1, "", StructureType.None);
            GameObject objectInDictionary = helper.AccessStructureInDictionary(gridPosition1);
            Assert.AreEqual(tempObject, objectInDictionary);
        }


        [Test]
        public void StructureDemolitionHelperCAncleDemolitionPasses()
        {
            helper.PrepareStructureForModification(gridPosition1, "", StructureType.None);
            helper.CancleModifications();
            Assert.IsTrue(grid.IsCellTaken(gridPosition1));
        }

        [Test]
        public void StructureDemolitionHelperConfirmForDemolitionPasses()
        {
            helper.PrepareStructureForModification(gridPosition1, "", StructureType.None);
            GameObject objectInDictionary = helper.AccessStructureInDictionary(gridPosition1);
            helper.ConfirmModifications();
            Assert.IsFalse(grid.IsCellTaken(gridPosition1));
        }
    }
}
