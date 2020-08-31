using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingRoadState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;
    public PlayerBuildingRoadState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }
    public override void OnConfirmAction()
    {
        
        this.buildingManager.ConfirmPlacement();
        base.OnConfirmAction();
    }
    public override void OnCancle()
    {
        this.buildingManager.CanclePlacement();
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }

    public override void EnterState(string structureName)
    {
        base.EnterState(structureName);
        this.structureName = structureName;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        buildingManager.PrepareStructureForPlacement(position, this.structureName, StructureType.Road);
    }

    public override void OnBuildArea(string structureName)
    {

        base.OnBuildArea(structureName);
        this.buildingManager.CanclePlacement();
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        base.OnBuildSingleStructure(structureName);
        this.buildingManager.CanclePlacement();
    }

}
