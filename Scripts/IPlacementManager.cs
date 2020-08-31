using System.Collections.Generic;
using UnityEngine;

public interface IPlacementManager
{
    GameObject CreateGhostStructure(Vector3 gridPosition, GameObject buildingPrefab);
    void DestroySingleStructure(GameObject structure);
    void DestroyStructures(IEnumerable<GameObject> structureCollection);
    void PlaceStructuresOnTheMap(IEnumerable<GameObject> structureCollection);
    void ResetBuildingLook(GameObject structure);
    void SetBuildingForDemolition(GameObject structureToDemolish);
}