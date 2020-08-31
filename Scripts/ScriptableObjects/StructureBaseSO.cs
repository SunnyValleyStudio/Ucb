using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureBaseSO : ScriptableObject
{
    public string buildingName;
    public GameObject prefab;
    public int placementCost;
    public int upkeepCost;
    [SerializeField]
    protected int income;
    public bool requireRoadAccess;
    public bool requireWater;
    public bool requirePower;

    public virtual int GetIncome()
    {
        return income;
    }
}
