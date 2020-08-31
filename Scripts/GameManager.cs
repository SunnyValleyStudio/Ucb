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

    private PlayerState state;

    public PlayerSelectionState selectionState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;

    //public PlayerState State { get => state; }

    private void Awake()
    {
        grid = new GridStructure(cellSize, width, length);
        selectionState = new PlayerSelectionState(this, cameraMovement);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, placementManager, grid);
        state = selectionState;
        state.EnterState();
    }
    void Start()
    {
        cameraMovement.SetCameraLimits(0, width, 0, length);
        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();
        
        inputManager.AddListenerOnPointerDownEvent(HandleInput);
        inputManager.AddListenerOnPointerSecondDownEvent(HandleInputCameraPan);
        inputManager.AddListenerOnPointerSecondUpEvent(HandleInputCameraStop);
        inputManager.AddListenerOnPointerChangeEvent(HandlePointerChange);
        uiController.AddListenerOnBuildAreaEvent(StartPlacementMode);
        uiController.AddListenerOnCancleActionEvent(CancelAction);
    }

    private void HandlePointerChange(Vector3 position)
    {
        state.OnInputPointerChange(position);
    }

    private void HandleInputCameraStop()
    {
        state.OnInputPanUp();
    }

    private void HandleInputCameraPan(Vector3 position)
    {
        state.OnInputPanChange(position);
    }

    private void HandleInput(Vector3 position)
    {
        state.OnInputPointerDown(position);

    }

    private void StartPlacementMode()
    {
        TransitionToState(buildingSingleStructureState);
    }

    private void CancelAction()
    {
        state.OnCancle();
    }
    public void TransitionToState(PlayerState newState)
    {
        this.state = newState;
        this.state.EnterState();
    }

}
