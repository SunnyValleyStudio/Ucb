using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RoadManagerTests
    {
        GridStructure grid;
        GameObject roadStraight = new GameObject();
        GameObject roadCorner = new GameObject();
        RoadStructureSO roadSO = new RoadStructureSO();
        GameObject road3Way = new GameObject();
        GameObject road4Way = new GameObject();

        [OneTimeSetUp]
        public void Init()
        {
            grid = new GridStructure(3, 10, 10);
            roadSO.prefab = roadStraight;
            roadSO.cornerPrefab = roadCorner;
            roadSO.threeWayPrefab = road3Way;
            roadSO.fourWayPrefab = road4Way;

            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(3, 0, 6), roadSO);
            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(6, 0, 3), roadSO);
            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(9, 0, 6), roadSO);
            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(6, 0, 9), roadSO);

            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(12, 0, 9), roadSO);
            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(15, 0, 6), roadSO);
        }

        [Test]
        public void RoadManagerTestsGetStatusNoNeighbours()
        {
            var result = RoadManager.GetRoadNeighboursStatus(new Vector3(27, 0, 27), grid, null);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void RoadManagerTestsGetStatusNeighbourRight()
        {
            var result = RoadManager.GetRoadNeighboursStatus(new Vector3(0, 0, 6), grid, null);
            Assert.AreEqual((int)Direction.Right, result);
        }

        [Test]
        public void RoadManagerTestsGetStatusNeighbourLeft()
        {
            var result = RoadManager.GetRoadNeighboursStatus(new Vector3(18, 0, 6), grid, null);
            Assert.AreEqual((int)Direction.Left, result);
        }

        [Test]
        public void RoadManagerTestsGetStatusNeighbourUp()
        {
            var result = RoadManager.GetRoadNeighboursStatus(new Vector3(6, 0, 0), grid, null);
            Assert.AreEqual((int)Direction.Up, result);
        }

        [Test]
        public void RoadManagerTestsGetStatusNeighbourDown()
        {
            var result = RoadManager.GetRoadNeighboursStatus(new Vector3(12, 0, 12), grid, null);
            Assert.AreEqual((int)Direction.Down, result);
        }

        [Test]
        public void RoadManagerTestsGetStatusNeighbourDownLeft()
        {
            var result = RoadManager.GetRoadNeighboursStatus(new Vector3(15, 0, 9), grid, null);
            Assert.AreEqual((int)Direction.Down | (int)Direction.Left, result);
        }

        [Test]
        public void RoadManagerTestsGetStatusNeighbourDownRight()
        {
            var result = RoadManager.GetRoadNeighboursStatus(new Vector3(3, 0, 9), grid, null);
            Assert.AreEqual((int)Direction.Down | (int)Direction.Right, result);
        }
        [Test]
        public void RoadManagerTestsGetStatusNeighbourUpRight()
        {
            var result = RoadManager.GetRoadNeighboursStatus(new Vector3(3, 0, 3), grid, null);
            Assert.AreEqual((int)Direction.Up | (int)Direction.Right, result);
        }
        [Test]
        public void RoadManagerTestsGetStatusNeighbourUpLeft()
        {
            var result = RoadManager.GetRoadNeighboursStatus(new Vector3(9, 0, 3), grid, null);
            Assert.AreEqual((int)Direction.Up | (int)Direction.Left, result);
        }
        [Test]
        public void RoadManagerTestsGetStatusNeighbourDownLeftRight()
        {
            var result = RoadManager.GetRoadNeighboursStatus(new Vector3(9, 0, 9), grid, null);
            Assert.AreEqual((int)Direction.Down | (int)Direction.Left | (int)Direction.Right, result);
        }
        [Test]
        public void RoadManagerTestsGetStatusNeighbourDownUpLeftRight()
        {
            var result = RoadManager.GetRoadNeighboursStatus(new Vector3(6, 0, 6), grid, null);
            Assert.AreEqual((int)Direction.Up | (int)Direction.Down | (int)Direction.Left | (int)Direction.Right, result);
        }

        [Test]
        public void RoadManagerTestsGetNeighboursAll4()
        {
            var result = RoadManager.GetRoadNeighboursForPosition(grid, new Vector3Int(6, 0, 6));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(3, 0, 6)));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(9, 0, 6)));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(6, 0, 3)));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(6, 0, 9)));
        }

        [Test]
        public void RoadManagerTestsGetNeighboursRightLeftDown()
        {
            var result = RoadManager.GetRoadNeighboursForPosition(grid, new Vector3Int(9, 0, 9));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(6, 0, 9)));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(9, 0, 6)));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(12, 0, 9)));
        }

        [Test]
        public void RoadManagerTestsGetNeighboursRight()
        {
            var result = RoadManager.GetRoadNeighboursForPosition(grid, new Vector3Int(0, 0, 6));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(3, 0, 6)));
        }

        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryTrue()
        {
            var dictionary = new Dictionary<Vector3Int, GameObject>();
            var position = new Vector3Int(3, 0, 6);
            dictionary.Add(position, roadStraight);
            var result = RoadManager.CheckIfNeighbourIsRoadInDictionary(position, dictionary);
            Assert.IsTrue(result);
        }

        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryFalse()
        {
            var dictionary = new Dictionary<Vector3Int, GameObject>();
            var position = new Vector3Int(3, 0, 6);
            dictionary.Add(position, roadStraight);
            var result = RoadManager.CheckIfNeighbourIsRoadInDictionary(new Vector3Int(6, 0, 6), dictionary);
            Assert.IsFalse(result);
        }

        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadOnGridTrue()
        {
            var position = new Vector3Int(3, 0, 6);
            var result = RoadManager.CheckIfNeighbourIsRoadOnTheGrid(grid, position);
            Assert.IsTrue(result);
        }

        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadOnGridFalse()
        {
            var position = new Vector3Int(0, 0, 0);
            var result = RoadManager.CheckIfNeighbourIsRoadOnTheGrid(grid, position);
            Assert.IsFalse(result);
        }

        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfStraightFits0()
        {
            var result = RoadManager.CheckIfStraightRoadFits(0, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }

        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfStraightFitsDownR90()
        {
            var result = RoadManager.CheckIfStraightRoadFits((int)Direction.Down, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R90, result.RoadPrefabRotation);
        }

        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfStraightFitsUpR90()
        {
            var result = RoadManager.CheckIfStraightRoadFits((int)Direction.Up, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R90, result.RoadPrefabRotation);
        }
        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfStraightFitsUpDownR90()
        {
            var result = RoadManager.CheckIfStraightRoadFits((int)Direction.Up | (int)Direction.Down, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R90, result.RoadPrefabRotation);
        }

        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfStraightRightFitsR0()
        {
            var result = RoadManager.CheckIfStraightRoadFits((int)Direction.Right, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }
        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfStraightLeftFitsR0()
        {
            var result = RoadManager.CheckIfStraightRoadFits((int)Direction.Left, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }
        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfStraightRightLeftFitsR0()
        {
            var result = RoadManager.CheckIfStraightRoadFits((int)Direction.Left | (int)Direction.Right, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }

        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfCornerFitsR0()
        {
            var result = RoadManager.CheckIfCornerFits((int)Direction.Up | (int)Direction.Right, null, roadSO);
            Assert.AreEqual(roadCorner, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }

        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfCornerFitsR90()
        {
            var result = RoadManager.CheckIfCornerFits((int)Direction.Down | (int)Direction.Right, null, roadSO);
            Assert.AreEqual(roadCorner, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R90, result.RoadPrefabRotation);
        }
        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfCornerFitsR180()
        {
            var result = RoadManager.CheckIfCornerFits((int)Direction.Down | (int)Direction.Left, null, roadSO);
            Assert.AreEqual(roadCorner, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R180, result.RoadPrefabRotation);
        }
        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfCornerFitsR270()
        {
            var result = RoadManager.CheckIfCornerFits((int)Direction.Up | (int)Direction.Left, null, roadSO);
            Assert.AreEqual(roadCorner, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R270, result.RoadPrefabRotation);
        }
        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfThreeWayFitsR0()
        {
            var result = RoadManager.CheckIf3WayFits((int)Direction.Up | (int)Direction.Right | (int)Direction.Down, null, roadSO);
            Assert.AreEqual(road3Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }
        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfThreeWayFitsR90()
        {
            var result = RoadManager.CheckIf3WayFits((int)Direction.Right | (int)Direction.Down | (int)Direction.Left, null, roadSO);
            Assert.AreEqual(road3Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R90, result.RoadPrefabRotation);
        }
        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfThreeWayFitsR180()
        {
            var result = RoadManager.CheckIf3WayFits((int)Direction.Down | (int)Direction.Left | (int)Direction.Up, null, roadSO);
            Assert.AreEqual(road3Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R180, result.RoadPrefabRotation);
        }
        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfThreeWayFitsR270()
        {
            var result = RoadManager.CheckIf3WayFits((int)Direction.Up | (int)Direction.Left | (int)Direction.Right, null, roadSO);
            Assert.AreEqual(road3Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R270, result.RoadPrefabRotation);
        }
        [Test]
        public void RoadManagerTestsCheckIfNeighbourIsRoadInDictionaryCheckIfFourWayFitsR0()
        {
            var result = RoadManager.CheckIf4WaysFits((int)Direction.Up | (int)Direction.Left | (int)Direction.Right | (int)Direction.Down, null, roadSO);
            Assert.AreEqual(road4Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }

        [Test]
        public void RoadManagerTestsGetCorrectRoadPrefab4WayGrid()
        {
            var result = RoadManager.GetCorrectRoadPrefab(new Vector3Int(6, 0, 6), roadSO, null, grid);
            Assert.AreEqual(road4Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }
        [Test]
        public void RoadManagerTestsGetCorrectRoadPrefabCornerDictionary()
        {
            var dictionary = new Dictionary<Vector3Int, GameObject>();
            dictionary.Add(new Vector3Int(3, 0, 0), roadStraight);
            dictionary.Add(new Vector3Int(0, 0, 3), roadStraight);
            var result = RoadManager.GetCorrectRoadPrefab(new Vector3Int(0, 0, 0), roadSO, dictionary, grid);
            Assert.AreEqual(roadCorner, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }
    }
}
