using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStructure
{
    public int cellSize = 3;
    public Vector3 CalculateGridPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt((float)inputPosition.x / cellSize);
        int z = Mathf.FloorToInt((float)inputPosition.z / cellSize);
        return new Vector3(x * cellSize, 0, z * cellSize);
    }


}
