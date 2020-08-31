using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingSingleStructureState : PlayerState
{
    PlacementManager placementManager;
    GridStructure grid;
    public PlayerBuildingSingleStructureState(GameManager gameManager,PlacementManager placementManager, GridStructure grid) : base(gameManager)
    {
        this.placementManager = placementManager;
        this.grid = grid;
    }
    public override void OnInputPanChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPanUp()
    {
        return;
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(position);
        if (grid.IsCellTaken(gridPosition) == false)
        {
            placementManager.CreateBuilding(gridPosition, grid);
        }
    }

    public override void OnInputPointerUp()
    {
        return;
    }

    public override void OnCancle()
    {
        this.gameManager.TransitionToState(this.gameManager.selectionState);
    }
}
