using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStructureInfoPanelHelper : MonoBehaviour
{
    public TextMeshProUGUI nameTxt, incomeTxt, upkeepTxt, clientsTxt;
    public Toggle powerToggle, waterToggle, roadToggle;

    void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //display all the data
    public void DisplayBasicStructureInfo(StructureBaseSO data)
    {
        Show();
        HideElement(clientsTxt.gameObject);
        HideElement(powerToggle.gameObject);
        HideElement(waterToggle.gameObject);
        HideElement(roadToggle.gameObject);
        SetText(nameTxt, data.buildingName);
        SetText(incomeTxt, data.GetIncome() + "");
        SetText(upkeepTxt, data.upkeepCost + "");
    }

    public void DisplayZoneStructureInfo(ZoneStructureSO data)
    {
        Show();
        HideElement(clientsTxt.gameObject);

        SetText(nameTxt, data.buildingName);
        SetText(incomeTxt, data.GetIncome() + "");
        SetText(upkeepTxt, data.upkeepCost + "");
        if (data.requirePower)
        {
            SetToggle(powerToggle, data.HasPower());
        }
        else
        {
            HideElement(powerToggle.gameObject);
        }
        if (data.requireWater)
        {
            SetToggle(waterToggle, data.HasWater());
        }
        else
        {
            HideElement(waterToggle.gameObject);
        }
        if (data.requireRoadAccess)
        {
            SetToggle(roadToggle, data.HasRoadAccess());
        }
        else
        {
            HideElement(roadToggle.gameObject);
        }
    }

    public void DisplayFacilityStructureInfo(SingleFacilitySO data)
    {
        Show();
        SetText(nameTxt, data.buildingName);
        SetText(incomeTxt, data.GetIncome() + "");
        SetText(upkeepTxt, data.upkeepCost + "");
        if (data.requirePower)
        {
            SetToggle(powerToggle, data.HasPower());
        }
        else
        {
            HideElement(powerToggle.gameObject);
        }
        if (data.requireWater)
        {
            SetToggle(waterToggle, data.HasWater());
        }
        else
        {
            HideElement(waterToggle.gameObject);
        }
        if (data.requireRoadAccess)
        {
            SetToggle(roadToggle, data.HasRoadAccess());
        }
        else
        {
            HideElement(roadToggle.gameObject);
        }
        SetText(clientsTxt, data.GetNumberOfCustomers() + "/" + data.maxCustomers);
    }

    private void HideElement(GameObject element)
    {
        element.transform.parent.gameObject.SetActive(false);
    }
    private void ShowElement(GameObject element)
    {
        element.transform.parent.gameObject.SetActive(true);
    }

    private void SetText(TextMeshProUGUI element, string value)
    {
        ShowElement(element.gameObject);
        element.text = value;
    }

    private void SetToggle(Toggle element, bool value)
    {
        ShowElement(element.gameObject);
        element.isOn = value;
    }
}
