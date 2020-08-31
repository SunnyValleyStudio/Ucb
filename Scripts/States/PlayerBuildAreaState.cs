using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildAreaState : PlayerState
{
    
    BuildingManager buildingManager;
    string structureName;

    public PlayerBuildAreaState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
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

        buildingManager.PlaceStructureAt(position);
    }

}
