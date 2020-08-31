using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class SingleStructureModificationHelperTests
    {
        GameObject tempObject = null;
        GridStructure grid;
        StructureType structureType = StructureType.Road;
        string structureName = "Road";
        Vector3 gridPosition1 = Vector3.zero;
        Vector3 gridPosition2 = new Vector3(3, 0, 3);
        StructureModificationHelper helper;

        [SetUp]
        public void Init()
        {
            StructureRepository structureRepository = TestHelpers.CreateStructureRepositoryContainingRoad();
            IPlacementManager placementManager = Substitute.For<IPlacementManager>();
            IResourceManager resourceManager = Substitute.For<IResourceManager>();
            resourceManager.CanIBuyIt(default).Returns(true);
            tempObject = new GameObject();
            placementManager.CreateGhostStructure(default, default).ReturnsForAnyArgs(tempObject);
            grid = new GridStructure(3, 10, 10);
            helper = new SingleStructurePlacementHelper(structureRepository, grid, placementManager, resourceManager);
        }
        // A Test behaves as an ordinary method
        [Test]
        public void SingleStructureModificationHelperAddPositionPasses()
        {
            helper.PrepareStructureForModification(gridPosition1, structureName, structureType);
            GameObject objectInDictionary = helper.AccessStructureInDictionary(gridPosition1);
            Assert.AreEqual(tempObject, objectInDictionary);
        }

        [Test]
        public void SingleStructureModificationHelperRemoveFromPositionsPasses()
        {
            helper.PrepareStructureForModification(gridPosition1, structureName, structureType);
            helper.PrepareStructureForModification(gridPosition1, structureName, structureType);
            GameObject objectInDictionary = helper.AccessStructureInDictionary(gridPosition1);
            Assert.IsNull(objectInDictionary);
        }

        [Test]
        public void SingleStructureModificationHelperAddToPositionsTwoTimesPasses()
        {
            helper.PrepareStructureForModification(gridPosition1, structureName, structureType);
            helper.PrepareStructureForModification(gridPosition2, structureName, structureType);
            GameObject objectInDictionary1 = helper.AccessStructureInDictionary(gridPosition1);
            GameObject objectInDictionary2 = helper.AccessStructureInDictionary(gridPosition2);
            Assert.AreEqual(tempObject, objectInDictionary1);
            Assert.AreEqual(tempObject, objectInDictionary2);
        }

        [Test]
        public void SingleStructureModificationHelperRemoveFromAllPositionsPasses()
        {
            helper.PrepareStructureForModification(gridPosition1, structureName, structureType);
            helper.PrepareStructureForModification(gridPosition2, structureName, structureType);
            helper.CancleModifications();
            GameObject objectInDictionary1 = helper.AccessStructureInDictionary(gridPosition1);
            GameObject objectInDictionary2 = helper.AccessStructureInDictionary(gridPosition2);
            Assert.IsNull(objectInDictionary1);
            Assert.IsNull(objectInDictionary2);
        }

        [Test]
        public void SingleStructureModificationHelperAddToGridPasses()
        {
            helper.PrepareStructureForModification(gridPosition1, structureName, structureType);
            helper.PrepareStructureForModification(gridPosition2, structureName, structureType);
            helper.ConfirmModifications();
            Assert.IsTrue(grid.IsCellTaken(gridPosition1));
            Assert.IsTrue(grid.IsCellTaken(gridPosition2));
        }

    }
}
