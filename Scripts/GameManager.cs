using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject placementManagerGameObject;
    private IPlacementManager placementManager;
    public StructureRepository structureRepository;
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
    public PlayerDemolitionState demolishState;
    public PlayerBuildingRoadState buildingRoadState;
    public PlayerBuildingZoneState buildingAreaState;

    public PlayerState State { get => state; }

    public GameObject resourceManagerGameObject;
    private IResourceManager resourceManager;

    private void Awake()
    {
        
#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDROID)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
#if (UNITY_IOS || UNITY_ANDROID)

#endif
    }

    private void PrepareStates()
    {
        buildingManager = new BuildingManager(cellSize, width, length, placementManager, structureRepository, resourceManager);
        resourceManager.PrepareResourceManager(buildingManager);
        selectionState = new PlayerSelectionState(this, buildingManager);
        demolishState = new PlayerDemolitionState(this, buildingManager);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager);
        buildingAreaState = new PlayerBuildingZoneState(this, buildingManager);
        buildingRoadState = new PlayerBuildingRoadState(this, buildingManager);
        state = selectionState;
        state.EnterState(null);
    }

    void Start()
    {
        placementManager = placementManagerGameObject.GetComponent<IPlacementManager>();
        resourceManager = resourceManagerGameObject.GetComponent<IResourceManager>();
        PrepareStates();
        PreapreGameComponents();
        AssignInputListeners();
        AssignUiControllerListeners();
    }

    private void PreapreGameComponents()
    {
        inputManager.MouseInputMask = inputMask;
        cameraMovement.SetCameraLimits(0, width, 0, length);
    }

    private void AssignUiControllerListeners()
    {
        uiController.AddListenerOnBuildAreaEvent((structureName) => state.OnBuildArea(structureName));
        uiController.AddListenerOnBuildSingleStructureEvent((structureName) => state.OnBuildSingleStructure(structureName));
        uiController.AddListenerOnBuildRoadEvent((structureName) => state.OnBuildRoad(structureName));
        uiController.AddListenerOnCancleActionEvent(() => state.OnCancle());
        uiController.AddListenerOnDemolishActionEvent(() => state.OnDemolishAction());
        uiController.AddListenerOnConfirmActionEvent(() => state.OnConfirmAction());

    }

    private void AssignInputListeners()
    {
        inputManager.AddListenerOnPointerDownEvent((position) => state.OnInputPointerDown(position));
        inputManager.AddListenerOnPointerSecondDownEvent((position) => state.OnInputPanChange(position));
        inputManager.AddListenerOnPointerSecondUpEvent(() => state.OnInputPanUp());
        inputManager.AddListenerOnPointerChangeEvent((position) => state.OnInputPointerChange(position));
        inputManager.AddListenerOnPointerUpEvent(() => state.OnInputPointerUp());
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

    private void StartPlacementMode(string variable)
    {
        TransitionToState(buildingSingleStructureState, variable);
    }

    public void TransitionToState(PlayerState newState, string variable)
    {
        this.state = newState;
        this.state.EnterState(variable);
    }

}
