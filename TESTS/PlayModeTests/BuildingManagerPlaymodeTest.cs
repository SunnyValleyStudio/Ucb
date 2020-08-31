using System;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class BuildingManagerPlaymodeTest
    {
        BuildingManager buildingManager;
        Material materialTransparent;

        [SetUp]
        public void InitBeforeEveryTests()
        {
            PlacementManager placementManager = Substitute.For<PlacementManager>();
            materialTransparent = new Material(Shader.Find("Standard"));
            placementManager.transparentMaterial = materialTransparent;
            GameObject ground = new GameObject();
            ground.transform.position = Vector3.zero;
            placementManager.ground = ground.transform;
            StructureRepository structureRepository = Substitute.For<StructureRepository>();
            CollectionSO collection = new CollectionSO();
            RoadStructureSO road = new RoadStructureSO();
            road.buildingName = "Road";
            GameObject roadChild = new GameObject("Road", typeof(MeshRenderer));
            roadChild.GetComponent<MeshRenderer>().material.color = Color.blue;
            GameObject roadPrefab = new GameObject("Road");
            roadChild.transform.SetParent(roadPrefab.transform);
            road.prefab = roadPrefab;
            collection.roadStructure = road;
            structureRepository.modelDataCollection = collection;
            buildingManager = new BuildingManager(3, 10, 10, placementManager, structureRepository);
        }
        [UnityTest]
        public IEnumerator BuildingManagerPlaymodeDemolishConfirmationTest()
        {
            Vector3 inputPosition = PreparePlacement();
            PrepareDemolition(inputPosition);
            buildingManager.PrepareBuildingManager(typeof(PlayerDemolitionState));
            buildingManager.ConfirmModification();
            yield return new WaitForEndOfFrame();
            Assert.IsNull(buildingManager.CheckForStructureInGrid(inputPosition));
        }



        [UnityTest]
        public IEnumerator BuildingManagerPlaymodeDemolishNoConfirmationTest()
        {
            Vector3 inputPosition = PreparePlacement();
            PrepareDemolition(inputPosition);
            yield return new WaitForEndOfFrame();
            Assert.IsNotNull(buildingManager.CheckForStructureInGrid(inputPosition));

        }

        [UnityTest]
        public IEnumerator BuildingManagerPlaymodeDemolishCancleTest()
        {
            Vector3 inputPosition = PreparePlacement();
            PrepareDemolition(inputPosition);
            buildingManager.PrepareBuildingManager(typeof(PlayerDemolitionState));
            buildingManager.CancelModification();
            yield return new WaitForEndOfFrame();
            Assert.IsNotNull(buildingManager.CheckForStructureInGrid(inputPosition));


        }

        [UnityTest]
        public IEnumerator BuildingManagerPlaymodePlacementCancleTests()
        {
            Vector3 inputPosition = PreparePlacement();
            buildingManager.CancelModification();
            yield return new WaitForEndOfFrame();
            Assert.IsNull(buildingManager.CheckForStructureInGrid(inputPosition));

        }

        [UnityTest]
        public IEnumerator BuildingManagerPlaymodePlacementConfirmationPassTests()
        {
            Vector3 inputPosition = PreparePlacement();
            buildingManager.ConfirmModification();
            yield return new WaitForEndOfFrame();
            Assert.IsNotNull(buildingManager.CheckForStructureInGrid(inputPosition));


        }

        [UnityTest]
        public IEnumerator BuildingManagerPlaymodePlacementNoConfirmationTests()
        {
            Vector3 inputPosition = PreparePlacement();
            yield return new WaitForEndOfFrame();
            Assert.IsNull(buildingManager.CheckForStructureInGrid(inputPosition));

        }

        [UnityTest]
        public IEnumerator BuildingManagerPlaymodeMaterialChangePlacementPrepareTests()
        {
            Vector3 inputPosition = PreparePlacement();
            Material material = AccessMaterial(() => buildingManager.CheckForStructureInDictionary(inputPosition));
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(material.color, Color.green);
        }

        [UnityTest]
        public IEnumerator BuildingManagerPlaymodeMaterialChangePlacementConfirmTests()
        {
            Vector3 inputPosition = PreparePlacement();
            buildingManager.ConfirmModification();
            Material material = AccessMaterial(() => buildingManager.CheckForStructureInGrid(inputPosition));
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(material.color, Color.blue);
        }



        [UnityTest]
        public IEnumerator BuildingManagerPlaymodeMaterialChangeDemolishPrepareTests()
        {
            Vector3 inputPosition = PreparePlacement();
            PrepareDemolition(inputPosition);
            Material material = AccessMaterial(() => buildingManager.CheckForStructureInDictionary(inputPosition));
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(material.color, Color.red);
        }

        [UnityTest]
        public IEnumerator BuildingManagerPlaymodeMaterialChangeDemolishCancleTests()
        {
            Vector3 inputPosition = PreparePlacement();
            PrepareDemolition(inputPosition);
            buildingManager.PrepareBuildingManager(typeof(PlayerDemolitionState));
            buildingManager.CancelModification();
            Material material = AccessMaterial(() => buildingManager.CheckForStructureInGrid(inputPosition));
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(material.color, Color.blue);
        }

        private Material AccessMaterial(Func<GameObject> accessMethod)
        {
            var roadObject = accessMethod();
            Material material = roadObject.GetComponentInChildren<MeshRenderer>().material;
            return material;
        }

        private Vector3 PreparePlacement()
        {
            Vector3 inputPosition = new Vector3(1, 0, 1);
            string structureName = "Road";
            buildingManager.PrepareBuildingManager(typeof(PlayerBuildingRoadState));
            buildingManager.PrepareStructureForPlacement(inputPosition, structureName, StructureType.Road);
            return inputPosition;
        }

        private void PrepareDemolition(Vector3 inputPosition)
        {
            buildingManager.ConfirmModification();
            buildingManager.PrepareBuildingManager(typeof(PlayerDemolitionState));
            buildingManager.PrepareStructureForDemolitionAt(inputPosition);
        }
    }
}
