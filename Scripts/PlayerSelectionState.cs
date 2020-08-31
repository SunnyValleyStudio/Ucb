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


    public override void OnCancle()
    {
        return;
    }
}
