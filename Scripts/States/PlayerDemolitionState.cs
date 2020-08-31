using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemolitionState : PlayerState
{
    BuildingManager buildingManager;
    public PlayerDemolitionState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }

    public override void OnCancle()
    {
        this.buildingManager.CancleDemolition();
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }

    public override void OnConfirmAction()
    {
        base.OnConfirmAction();
        this.buildingManager.ConfirmDemolition();
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        this.buildingManager.CancleDemolition();
        base.OnBuildSingleStructure(structureName);
    }

    public override void OnBuildRoad(string structureName)
    {
        this.buildingManager.CancleDemolition();
        base.OnBuildRoad(structureName);
    }

    public override void OnBuildArea(string structureName)
    {
        this.buildingManager.CancleDemolition();
        base.OnBuildArea(structureName);
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        this.buildingManager.PrepareStructureForDemolitionAt(position);
    }

}
