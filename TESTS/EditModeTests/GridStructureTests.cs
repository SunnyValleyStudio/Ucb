using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class GridStructureTests
    {
        GridStructure grid;
        [SetUp]
        public void Init()
        {
            grid = new GridStructure(3, 100, 100);
        }
        #region GridPositionTests
        // A Test behaves as an ordinary method
        [Test]
        public void CalculateGridPositionPasses()
        {
            Vector3 position = new Vector3(0, 0, 0);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            //Assert
            Assert.AreEqual(Vector3.zero, returnPosition);
        }

        [Test]
        public void CalculateGridPositionFloatsPasses()
        {

            Vector3 position = new Vector3(2.9f, 0, 2.9f);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            //Assert
            Assert.AreEqual(Vector3.zero, returnPosition);
        }

        [Test]
        public void CalculateGridPositionFail()
        {

            Vector3 position = new Vector3(3.1f, 0, 0);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            //Assert
            Assert.AreNotEqual(Vector3.zero, returnPosition);
        }
        #endregion


        #region GridIndexTests
        //[Test]
        //public void CalculateGridIndexFromGridPosition000Passes()
        //{

        //    Vector3 position = new Vector3(0, 0, 0);
        //    //Act
        //    Vector3 returnPosition = structure.CalculateGridPosition(position);
        //    Vector2Int indexInsideGrid = structure.CalculateGridIndex(returnPosition);
        //    //Assert
        //    Assert.AreEqual(Vector2Int.zero, indexInsideGrid);
        //}
        //[Test]
        //public void CalculateGridIndexFromGridPosition303Passes()
        //{

        //    Vector3 position = new Vector3(3, 0, 3);
        //    //Act
        //    Vector3 returnPosition = structure.CalculateGridPosition(position);
        //    Vector2Int indexInsideGrid = structure.CalculateGridIndex(returnPosition);
        //    //Assert
        //    Assert.AreEqual(new Vector2Int(1,1), indexInsideGrid);
        //}
        //[Test]
        //public void CalculateGridIndexFromGridPosition301Fail()
        //{

        //    Vector3 position = new Vector3(3, 0, 1);
        //    //Act
        //    Vector3 returnPosition = structure.CalculateGridPosition(position);
        //    Vector2Int indexInsideGrid = structure.CalculateGridIndex(returnPosition);
        //    //Assert
        //    Assert.AreNotEqual(new Vector2Int(1, 1), indexInsideGrid);
        //}
        #endregion

        #region GridCellTests
        [Test]
        public void PlaceStructure303AndCheckIsTakenPasses()
        {

            Vector3 position = new Vector3(3, 0, 3);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            GameObject testGameObject = new GameObject("TestGameObject");
            grid.PlaceStructureOnTheGrid(testGameObject, position, null);
            //Assert
            Assert.IsTrue(grid.IsCellTaken(position));
        }
        [Test]
        public void PlaceStructureMINAndCheckIsTakenPasses()
        {

            Vector3 position = new Vector3(0, 0, 0);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            GameObject testGameObject = new GameObject("TestGameObject");
            grid.PlaceStructureOnTheGrid(testGameObject, position, null);
            //Assert
            Assert.IsTrue(grid.IsCellTaken(position));
        }
        [Test]
        public void PlaceStructureMAXAndCheckIsTakenPasses()
        {

            Vector3 position = new Vector3(297, 0, 297);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            GameObject testGameObject = new GameObject("TestGameObject");
            grid.PlaceStructureOnTheGrid(testGameObject, position, null);
            //Assert
            Assert.IsTrue(grid.IsCellTaken(position));
        }

        [Test]
        public void PlaceStructure303AndCheckIsTakenNullObjectShouldFail()
        {

            Vector3 position = new Vector3(3, 0, 3);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            GameObject testGameObject = null;
            grid.PlaceStructureOnTheGrid(testGameObject, position, null);
            //Assert
            Assert.IsFalse(grid.IsCellTaken(position));
        }

        [Test]
        public void PlaceStructureAndCheckIsTakenIndexOutOfBoundsFail()
        {

            Vector3 position = new Vector3(303, 0, 303);
            //Act
            //Assert
            Assert.Throws<IndexOutOfRangeException>(() => grid.IsCellTaken(position));
        }

        [Test]
        public void RetreiveStructureFromCellGameObjectPasses()
        {

            Vector3 position = new Vector3(297, 0, 297);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            GameObject testGameObject = new GameObject("TestGameObject");
            grid.PlaceStructureOnTheGrid(testGameObject, position, null);
            GameObject retreivedGameObject = grid.GetStructureFromTheGrid(position);
            //Assert
            Assert.AreEqual(testGameObject, retreivedGameObject);
        }

        [Test]
        public void RetreiveStructureFromCellNullPasses()
        {

            Vector3 position = new Vector3(297, 0, 297);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            //clean up after any other test
            grid.RemoveStructureFromTheGrid(position);
            GameObject retreivedGameObject = grid.GetStructureFromTheGrid(position);
            //Assert
            Assert.AreEqual(null, retreivedGameObject);
        }

        [Test]
        public void GetPositionOfTheNeighbourIfExistsTestPass()
        {

            Vector3 position = new Vector3(0, 0, 0);

            var neighbourPosition = grid.GetPositionOfTheNeighbourIfExists(position, Direction.Up);
            Assert.AreEqual(new Vector3Int(0, 0, 3), neighbourPosition.Value);
        }

        [Test]
        public void GetPositionOfTheNeighbourIfExistsTestFail()
        {

            Vector3 position = new Vector3(0, 0, 0);

            var neighbourPosition = grid.GetPositionOfTheNeighbourIfExists(position, Direction.Down);
            Assert.IsFalse(neighbourPosition.HasValue);
        }

        #endregion

        [Test]
        public void GetAllPositionFromTo()
        {

            Vector3Int startPosition = new Vector3Int(0, 0, 0);
            Vector3Int endPosition = new Vector3Int(6, 0, 3);

            var returnValues = grid.GetAllPositionsFromTo(startPosition, endPosition);
            Assert.IsTrue(returnValues.Count == 6);
            Assert.IsTrue(returnValues.Contains(new Vector3Int(0, 0, 0)));
            Assert.IsTrue(returnValues.Contains(new Vector3Int(3, 0, 0)));
            Assert.IsTrue(returnValues.Contains(new Vector3Int(6, 0, 0)));
            Assert.IsTrue(returnValues.Contains(new Vector3Int(0, 0, 3)));
            Assert.IsTrue(returnValues.Contains(new Vector3Int(3, 0, 3)));
            Assert.IsTrue(returnValues.Contains(new Vector3Int(6, 0, 3)));
        }

        [Test]
        public void GetDataStructureTest()
        {
            RoadStructureSO road = ScriptableObject.CreateInstance<RoadStructureSO>();
            SingleStructureBaseSO singleStructure = ScriptableObject.CreateInstance<SingleFacilitySO>();
            GameObject gameObject = new GameObject();
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(0, 0, 0), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 0), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(0, 0, 3), singleStructure);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 3), singleStructure);
            var list = grid.GetAllStructures().ToList();
            Assert.IsTrue(list.Count == 4);
        }

        [Test]
        public void GetDataStructureInRange1Contains4Test()
        {
            RoadStructureSO road = new RoadStructureSO();
            SingleStructureBaseSO singleStructure = new SingleFacilitySO();
            GameObject gameObject = new GameObject();
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(6, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 9), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(6, 0, 9), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 9), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 6), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 6), road);
            var list = grid.GetStructuresDataInRange(new Vector3(6, 0, 6), 1).ToList();
            Assert.IsTrue(list.Count == 4);
        }

        [Test]
        public void GetDataStructureInRange1Contains2Test()
        {
            RoadStructureSO road = new RoadStructureSO();
            SingleStructureBaseSO singleStructure = new SingleFacilitySO();
            GameObject gameObject = new GameObject();
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(6, 0, 3), singleStructure);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 9), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(6, 0, 9), singleStructure);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 9), road);
            var list = grid.GetStructuresDataInRange(new Vector3(6, 0, 6), 1).ToList();
            Assert.IsTrue(list.Count == 2);
            Assert.IsTrue(list[0] == singleStructure);
            Assert.IsTrue(list[1] == singleStructure);
        }
        [Test]
        public void GetDataStructureInRange1Contains3Test()
        {
            RoadStructureSO road = new RoadStructureSO();
            SingleStructureBaseSO singleStructure = new SingleFacilitySO();
            GameObject gameObject = new GameObject();
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(6, 0, 3), singleStructure);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 9), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(6, 0, 9), singleStructure);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 9), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 6), road);
            var list = grid.GetStructuresDataInRange(new Vector3(6, 0, 6), 1).ToList();
            Assert.IsTrue(list.Count == 3);
            Assert.IsTrue(list.Contains(singleStructure));
            Assert.IsTrue(list.Contains(road));
        }
        [Test]
        public void GetDataStructureInRange1Contains0Test()
        {
            RoadStructureSO road = new RoadStructureSO();
            SingleStructureBaseSO singleStructure = new SingleFacilitySO();
            GameObject gameObject = new GameObject();
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 9), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 9), road);
            var list = grid.GetStructuresDataInRange(new Vector3(6, 0, 6), 1).ToList();
            Assert.IsTrue(list.Count == 0);
        }

        [Test]
        public void GetDataStructureInRange2Contains8Test()
        {
            RoadStructureSO road = new RoadStructureSO();
            SingleStructureBaseSO singleStructure = new SingleFacilitySO();
            GameObject gameObject = new GameObject();
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(6, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 3), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 9), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(6, 0, 9), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 9), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 6), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(9, 0, 6), road);
            var list = grid.GetStructuresDataInRange(new Vector3(6, 0, 6), 2).ToList();
            Assert.IsTrue(list.Count == 8);
        }
    }
}
