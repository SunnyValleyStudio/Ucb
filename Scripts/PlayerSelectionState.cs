using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionState : PlayerState
{
    CameraMovement cameraMovement;
    public PlayerSelectionState(GameManager gameManager, CameraMovement cameraMovement) : base(gameManager)
    {
        this.cameraMovement = cameraMovement;
    }
    public override void OnInputPanChange(Vector3 panPosition)
    {
        cameraMovement.MoveCamera(panPosition);
    }

    public override void OnInputPanUp()
    {
        cameraMovement.StopCameraMovement();
    }

    public virtual void OnBuildArea(string structureName)
    {
        this.gameManager.TransitionToState(this.gameManager.buildingAreaState, structureName);
    }

    public virtual void OnBuildSingleStructure(string structureName)
    {
        this.gameManager.TransitionToState(this.gameManager.buildingSingleStructureState, structureName);
    }

    public virtual void OnBuildRoad(string structureName)
    {
        this.gameManager.TransitionToState(this.gameManager.buildingRoadState, structureName);
    }

    public override void OnDemolishAction()
    {
        this.gameManager.TransitionToState(this.gameManager.demolishState, null);
    }

    public override void OnCancle()
    {
        throw new System.NotImplementedException();
    }
}
