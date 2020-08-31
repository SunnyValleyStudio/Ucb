using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ZoneCalculator
{

    public static bool CheckIfPositionHasChanged(Vector3 gridPosition, Vector3 previousPosition, GridStructure grid)
    {
        return Vector3Int.FloorToInt(gridPosition).Equals(Vector3Int.FloorToInt(previousPosition)) == false;
    }

}
