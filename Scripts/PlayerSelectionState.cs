using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionState : PlayerState
{
    BuildingManager buildingManager;
    Vector3? previousPosition;

    public PlayerSelectionState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        StructureBaseSO data = buildingManager.GetStructureDataFromPosition(position);
        if (data)
        {
            UpdateStructureInfoPanel(data);
            previousPosition = position;
        }
        else
        {
            this.gameManager.uiController.HideStructureInfo();
            data = null;
            previousPosition = null;
        }
        return;
    }

    private void UpdateStructureInfoPanel(StructureBaseSO data)
    {
        Type dataType = data.GetType();
        if (dataType == typeof(SingleFacilitySO))
        {
            this.gameManager.uiController.DisplayFacilitStructureInfo((SingleFacilitySO)data);
        }
        else if (dataType == typeof(ZoneStructureSO))
        {
            this.gameManager.uiController.DisplayZoneStructureInfo((ZoneStructureSO)data);
        }
        else
        {
            this.gameManager.uiController.DisplayBasicStructureInfo(data);
        }
    }

    public override void OnInputPointerUp()
    {
        return;
    }

    public override void OnCancle()
    {
        return;
    }

    public override void EnterState(string variable)
    {
        if (this.gameManager.uiController.GetStructureInfoVisibility())
        {
            StructureBaseSO data = buildingManager.GetStructureDataFromPosition(previousPosition.Value);
            if (data)
            {
                UpdateStructureInfoPanel(data);
            }
            else
            {
                this.gameManager.uiController.HideStructureInfo();
                previousPosition = null;
            }


        }

    }

}
