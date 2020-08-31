using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject buildingPrefab;
    public Transform ground;

    public void CreateBuilding(Vector3 gridPosition, GridStructure grid)
    {
        GameObject newStructure = Instantiate(buildingPrefab, ground.position + gridPosition, Quaternion.identity);
        grid.PlaceStructureOnTheGrid(newStructure, gridPosition);
    }

    public void RemoveBuilding(Vector3 gridPosition, GridStructure grid)
    {
        var structure = grid.GetStructureFromTheGrid(gridPosition);
        if (structure != null)
        {
            Destroy(structure);
            grid.RemoveStructureFromTheGrid(gridPosition);
        }
    }
}
