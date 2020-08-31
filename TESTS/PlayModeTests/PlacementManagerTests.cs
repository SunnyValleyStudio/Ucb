using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class PlacementManagerTests
    {
        Material materialTransparent;
        PlacementManager placementManager;
        GameObject testGameObject;
        Vector3 gridPosition1 = Vector3.zero;
        Vector3 gridPosition2 = new Vector3(3, 0, 3);

        [SetUp]
        public void Init()
        {
            GameObject ground = new GameObject();
            ground.transform.position = Vector3.zero;
            testGameObject = TestHelpers.GetAGameObjectWithMaterial();
            materialTransparent = new Material(Shader.Find("Standard"));

            placementManager = Substitute.For<PlacementManager>();
            placementManager.ground = ground.transform;
            placementManager.transparentMaterial = materialTransparent;
        }
        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator PlacementManagerTestsCreateGhostStructurePasses()
        {
            GameObject ghostObject = placementManager.CreateGhostStructure(gridPosition1, testGameObject);
            yield return new WaitForEndOfFrame();
            Color color = Color.green;
            color.a = 0.5f;
            foreach (var renderer in ghostObject.GetComponentsInChildren<MeshRenderer>())
            {
                
                Assert.AreEqual(renderer.material.color, color);
            }
        }

        [UnityTest]
        public IEnumerator PlacementManagerPlaceStructureOnTheMapMaterialPasses()
        {
            GameObject ghostObject = placementManager.CreateGhostStructure(gridPosition1, testGameObject);
            placementManager.PlaceStructuresOnTheMap(new List<GameObject>() { ghostObject });
            yield return new WaitForEndOfFrame();
            foreach (var renderer in ghostObject.GetComponentsInChildren<MeshRenderer>())
            {
                Assert.AreEqual(renderer.material.color, Color.blue);
            }

        }

        [UnityTest]
        public IEnumerator PlacementManagerDestroyStructurePasses()
        {
            placementManager.SetBuildingForDemolition(testGameObject);
            yield return new WaitForEndOfFrame();
            Color color = Color.red;
            color.a = 0.5f;
            foreach (var renderer in testGameObject.GetComponentsInChildren<MeshRenderer>())
            {
                
                Assert.AreEqual(renderer.material.color, color);
            }


        }
    }
}
