using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    GameObject structureModel = null;
    bool isTaken = false;

    public bool IsTaken { get => isTaken;}

    public void SetConstruction(GameObject structureModel)
    {
        if (structureModel == null)
            return;
        this.structureModel = structureModel;
        this.isTaken = true;
    }
}
