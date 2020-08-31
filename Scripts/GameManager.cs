using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public IInputManager inputManager;
    public UiController uiController;
    public int width, length;
    public CameraMovement cameraMovement;
    public GridStructure grid;
    private int cellSize = 3;

    private bool buildingModeActive = false;

    void Start()
    {
        cameraMovement.SetCameraLimits(0, width, 0, length);
        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();
        grid = new GridStructure(cellSize, width, length);
        inputManager.AddListenerOnPointerDownEvent(HandleInput);
        inputManager.AddListenerOnPointerSecondDownEvent(HandleInputCameraPan);
        inputManager.AddListenerOnPointerSecondUpEvent(HandleInputCameraStop);
        inputManager.AddListenerOnPointerChangeEvent(HandlePointerChange);
        uiController.AddListenerOnBuildAreaEvent(StartPlacementMode);
        uiController.AddListenerOnCancleActionEvent(CancelAction);
    }

    private void HandlePointerChange(Vector3 position)
    {
        Debug.Log(position);
    }

    private void HandleInputCameraStop()
    {
        cameraMovement.StopCameraMovement();
    }

    private void HandleInputCameraPan(Vector3 position)
    {
        if (buildingModeActive == false)
        {
            cameraMovement.MoveCamera(position);
        }
    }

    private void HandleInput(Vector3 position)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(position);
        if (buildingModeActive && grid.IsCellTaken(gridPosition) == false)
        {
            placementManager.CreateBuilding(gridPosition, grid);
        }

    }

    private void StartPlacementMode()
    {
        buildingModeActive = true;
    }

    private void CancelAction()
    {
        buildingModeActive = false;
    }

}
