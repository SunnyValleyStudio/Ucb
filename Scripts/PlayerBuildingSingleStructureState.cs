using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingSingleStructureState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;
    public PlayerBuildingSingleStructureState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }
    public override void OnConfirmAction()
    {
        base.OnConfirmAction();
        this.buildingManager.ConfirmPlacement();
    }
    public override void OnInputPointerDown(Vector3 position)
    {

        buildingManager.PlaceStructureAt(position, this.structureName, StructureType.SingleStructure);
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
}
