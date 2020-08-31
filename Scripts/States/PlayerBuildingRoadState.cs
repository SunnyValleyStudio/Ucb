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
        base.OnConfirmAction();
        this.buildingManager.ConfirmPlacement();
    }
    public override void OnCancle()
    {
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }

    public override void EnterState(string structureName)
    {
        base.EnterState(structureName);
        this.structureName = structureName;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        buildingManager.PlaceStructureAt(position, this.structureName, StructureType.Road);
    }
}
