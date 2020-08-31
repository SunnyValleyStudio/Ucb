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
            tempObject = new GameObject();
            placementManager.CreateGhostStructure(default, default).ReturnsForAnyArgs(tempObject);
            grid = new GridStructure(3, 10, 10);
            helper = new SingleStructurePlacementHelper(structureRepository, grid, placementManager);
        }
        // A Test behaves as an ordinary method
        [Test]
        public void SingleStructureModificationHelperAddPositionPasses()
        {
            helper.PrepareStructureForModification(gridPosition1, structureName, structureType);
            GameObject objectInDictionary = helper.AccessStructureInDictionary(gridPosition1);
            Assert.AreEqual(tempObject, objectInDictionary);
        }

    }
}
