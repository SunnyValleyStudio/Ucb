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
    public LayerMask inputMask;
    private BuildingManager buildingManager;
    private int cellSize = 3;

    private PlayerState state;

    public PlayerSelectionState selectionState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;
    public PlayerRemoveBuildingState demolishState;

    public PlayerState State { get => state; }

    private void Awake()
    {
        buildingManager = new BuildingManager(cellSize, width, length, placementManager);
        selectionState = new PlayerSelectionState(this, cameraMovement);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager);
        demolishState = new PlayerRemoveBuildingState(this, buildingManager);
        state = selectionState;
        state.EnterState();
#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDROID)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
#if (UNITY_IOS || UNITY_ANDROID)

#endif
    }
    void Start()
    {
        PreapreGameComponents();
        //inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();

        AssignInputListeners();
        AssignUiControllerListeners();
    }

    private void PreapreGameComponents()
    {
        inputManager.MouseInputMask = inputMask;
        cameraMovement.SetCameraLimits(0, width, 0, length);
    }

    private void AssignInputListeners()
    {
        inputManager.AddListenerOnPointerDownEvent(HandleInput);
        inputManager.AddListenerOnPointerSecondDownEvent(HandleInputCameraPan);
        inputManager.AddListenerOnPointerSecondUpEvent(HandleInputCameraStop);
        inputManager.AddListenerOnPointerChangeEvent(HandlePointerChange);
    }

    private void AssignUiControllerListeners()
    {
        uiController.AddListenerOnBuildAreaEvent(StartPlacementMode);
        uiController.AddListenerOnCancleActionEvent(CancelAction);
        uiController.AddListenerOnDemolishActionEvent(StartDemolishMode);
    }

    private void StartDemolishMode()
    {
        TransitionToState(demolishState);
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
