using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    [TestFixture]
    public class PlayerStatusTest
    {
        UiController uiController;
        GameManager gameManagerComponent;

        //[SetUp]
        //public void Init()
        //{
        //    GameObject gameManagerObject = new GameObject();
        //    var camerMovementComponent = gameManagerObject.AddComponent<CameraMovement>();

        //    uiController = gameManagerObject.AddComponent<UiController>();
        //    GameObject buttonBuildObject = new GameObject();
        //    GameObject cancleButtonObject = new GameObject();
        //    GameObject canclePane = new GameObject();
        //    uiController.cancleActionBtn = cancleButtonObject.AddComponent<Button>();
        //    var buttonBuildComponent = buttonBuildObject.AddComponent<Button>();
        //    uiController.buildResidentialAreaBtn = buttonBuildComponent;
        //    uiController.cancleActionPanel = cancleButtonObject;

        //    uiController.buildingMenuPanel = canclePane;
        //    uiController.openBuildMenuBtn = uiController.cancleActionBtn;
        //    uiController.demolishBtn = uiController.cancleActionBtn;

        //    gameManagerComponent = gameManagerObject.AddComponent<GameManager>();
        //    gameManagerComponent.cameraMovement = camerMovementComponent;
        //    gameManagerComponent.uiController = uiController;
        //}

        [SetUp]
        public void Init()
        {
            GameObject gameManagerObject = new GameObject();
            var camerMovementComponent = gameManagerObject.AddComponent<CameraMovement>();
            gameManagerObject.AddComponent<ResourceManagerTestStud>();

            uiController = Substitute.For<UiController>();

            gameManagerComponent = gameManagerObject.AddComponent<GameManager>();
            gameManagerComponent.resourceManagerGameObject = gameManagerObject;
            gameManagerObject.AddComponent<PlacementManager>();
            gameManagerComponent.placementManagerGameObject = gameManagerObject;
            gameManagerComponent.cameraMovement = camerMovementComponent;
            gameManagerComponent.uiController = uiController;
        }

        [UnityTest]
        public IEnumerator PlayerStatusPlayerBuildingSingleStructureStateTestWithEnumeratorPasses()
        {
            yield return new WaitForEndOfFrame(); //awake
            yield return new WaitForEndOfFrame(); //start
            gameManagerComponent.State.OnBuildSingleStructure(null);
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(gameManagerComponent.State is PlayerBuildingSingleStructureState);

        }

        [UnityTest]
        public IEnumerator PlayerStatusPlayerBuildAreaStateTestWithEnumeratorPasses()
        {
            yield return new WaitForEndOfFrame(); //awake
            yield return new WaitForEndOfFrame(); //start
            gameManagerComponent.State.OnBuildArea(null);
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(gameManagerComponent.State is PlayerBuildingZoneState);

        }

        [UnityTest]
        public IEnumerator PlayerStatusPlayerBuildingRoadStateTestWithEnumeratorPasses()
        {
            yield return new WaitForEndOfFrame(); //awake
            yield return new WaitForEndOfFrame(); //start
            gameManagerComponent.State.OnBuildRoad(null);
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(gameManagerComponent.State is PlayerBuildingRoadState);

        }

        [UnityTest]
        public IEnumerator PlayerStatusPlayerRemoveBuildingStateTestWithEnumeratorPasses()
        {
            yield return new WaitForEndOfFrame(); //awake
            yield return new WaitForEndOfFrame(); //start
            gameManagerComponent.State.OnDemolishAction();
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(gameManagerComponent.State is PlayerDemolitionState);

        }

        [UnityTest]
        public IEnumerator PlayerStatusPlayerSelectionStateTestWithEnumeratorPasses()
        {
            yield return new WaitForEndOfFrame(); //awake
            yield return new WaitForEndOfFrame(); //start
            Assert.IsTrue(gameManagerComponent.State is PlayerSelectionState);

        }
    }
}
