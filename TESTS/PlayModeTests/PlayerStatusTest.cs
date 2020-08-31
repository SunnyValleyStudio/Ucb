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

        [SetUp]
        public void Init()
        {
            GameObject gameManagerObject = new GameObject();
            var camerMovementComponent = gameManagerObject.AddComponent<CameraMovement>();
            uiController = gameManagerObject.AddComponent<UiController>();
            GameObject buttonBuildObject = new GameObject();
            GameObject cancleButtonObject = new GameObject();
            GameObject canclePane = new GameObject();
            uiController.cancleActionBtn = cancleButtonObject.AddComponent<Button>();
            var buttonBuildComponent = buttonBuildObject.AddComponent<Button>();
            uiController.buildResidentialAreaBtn = buttonBuildComponent;
            uiController.cancleActionPanel = cancleButtonObject;

            uiController.buildingMenuPanel = canclePane;
            uiController.openBuildMenuBtn = uiController.cancleActionBtn;
            uiController.demolishBtn = uiController.cancleActionBtn;

            gameManagerComponent = gameManagerObject.AddComponent<GameManager>();
            gameManagerComponent.cameraMovement = camerMovementComponent;
            gameManagerComponent.uiController = uiController;
        }


        [UnityTest]
        public IEnumerator PlayerStatusPlayerBuildingSingleStructureStateTestWithEnumeratorPasses()
        {
            yield return new WaitForEndOfFrame(); //awake
            yield return new WaitForEndOfFrame(); //start
            uiController.buildResidentialAreaBtn.onClick.Invoke();
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(gameManagerComponent.State is PlayerBuildingSingleStructureState);

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
