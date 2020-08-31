using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ZoneCalculator
{

    public static bool CheckIfPositionHasChanged(Vector3 gridPosition, Vector3 previousPosition, GridStructure grid)
    {
        return Vector3Int.FloorToInt(gridPosition).Equals(Vector3Int.FloorToInt(previousPosition)) == false;
    }

    internal static void PrepareStartAndEndPosition(Vector3 startPosition, Vector3 endPosition, ref Vector3Int minPoint, ref Vector3Int maxPoint, Vector3 mapBottomLeftCorner)
    {
        throw new NotImplementedException();
    }
}
