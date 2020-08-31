using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingAreaState : PlayerState
{

    BuildingManager buildingManager;
    public PlayerBuildingAreaState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }

    public override void OnCancle()
    {
        throw new System.NotImplementedException();
    }
}
