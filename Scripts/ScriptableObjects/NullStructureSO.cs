using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullStructureSO : StructureBaseSO
{
    private void OnEnable()
    {
        buildingName = "nullable object";
        prefab = null;
        placementCost = 0;
        upkeepCost = 0;
        income = 0;
        requireRoadAccess = false;
        requireWater = false;
        requirePower = false;
    }

}
