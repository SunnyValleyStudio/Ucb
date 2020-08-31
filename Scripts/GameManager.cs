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
    public GridStructure grid;
    private int cellSize = 3;

    private bool buildingModeActive = false;

    void Start()
    {
        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();
        grid = new GridStructure(cellSize, width, length);
        inputManager.AddListenerOnPointerDownEvent(HandleInput);
        uiController.AddListenerOnBuildAreaEvent(StartPlacementMode);
        uiController.AddListenerOnCancleActionEvent(CancelAction);
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
