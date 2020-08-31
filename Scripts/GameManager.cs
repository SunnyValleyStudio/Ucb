using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public InputManager inputManager;
    public int width, length;
    public GridStructure grid;
    private int cellSize = 3;


    void Start()
    {
        grid = new GridStructure(cellSize, width, length);
        inputManager.AddListenerOnPointerDownEvent(HandleInput);
    }

    private void HandleInput(Vector3 position)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(position);
        if (grid.IsCellTaken(gridPosition) == false)
        {
            placementManager.CreateBuilding(gridPosition, grid);
        }

    }

}
