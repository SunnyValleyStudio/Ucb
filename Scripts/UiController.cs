using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    private Action<string> OnBuildAreaHandler;
    private Action<string> OnBuildSingleStructureHandler;
    private Action<string> OnBuildRoadHandler;

    private Action OnCancleActionHandler;
    private Action OnConfirmActionHandler;
    private Action OnDemolishActionHandler;

    public StructureRepository structureRepository;
    public Button buildResidentialAreaBtn;
    public Button cancleActionBtn;
    public Button confirmActionBtn;
    public GameObject cancleActionPanel;

    public GameObject buildingMenuPanel;
    public Button openBuildMenuBtn;
    public Button demolishBtn;

    public GameObject zonesPanel;
    public GameObject facilitiesPanel;

    public void HideStructureInfo()
    {
        structurePanelHelper.Hide();
    }

    public GameObject roadsPanel;
    public Button closeBuildMenuBtn;

    public GameObject buildButtonPrefab;

    public TextMeshProUGUI moneyValue;
    public TextMeshProUGUI populationValue;

    public UIStructureInfoPanelHelper structurePanelHelper;

    // Start is called before the first frame update
    void Start()
    {
        cancleActionPanel.SetActive(false);
        buildingMenuPanel.SetActive(false);
        //buildResidentialAreaBtn.onClick.AddListener(OnBuildAreaCallback);
        cancleActionBtn.onClick.AddListener(OnCancleActionCallback);
        confirmActionBtn.onClick.AddListener(OnConfirmActionCallback);
        openBuildMenuBtn.onClick.AddListener(OnOpenBuildMenu);
        demolishBtn.onClick.AddListener(OnDemolishHandler);
        closeBuildMenuBtn.onClick.AddListener(OnCloseMenuHandler);
    }

    private void OnConfirmActionCallback()
    {
        cancleActionPanel.SetActive(false);
        OnConfirmActionHandler?.Invoke();
    }

    internal bool GetStructureInfoVisibility()
    {
        return structurePanelHelper.gameObject.activeSelf;
    }

    private void OnCloseMenuHandler()
    {
        buildingMenuPanel.SetActive(false);
    }

    private void OnDemolishHandler()
    {
        OnDemolishActionHandler?.Invoke();
        cancleActionPanel.SetActive(true);
        OnCloseMenuHandler();
    }

    private void OnOpenBuildMenu()
    {
        buildingMenuPanel.SetActive(true);
        PrepareBuildMenu();
    }

    private void PrepareBuildMenu()
    {
        CreateButtonsInPanel(zonesPanel.transform, structureRepository.GetZoneNames(), OnBuildAreaCallback);
        CreateButtonsInPanel(facilitiesPanel.transform, structureRepository.GetSingleStructureNames(), OnBuildSingleStructureCallback);
        CreateButtonsInPanel(roadsPanel.transform, new List<string>() { structureRepository.GetRoadStructureName() }, OnBuildRoadCallback);
    }

    public void SetPopulationValue(int population)
    {
        populationValue.text = population + "";
    }

    private void OnBuildRoadCallback(string nameOfStructure)
    {
        PrepareUIForBuilding();
        OnBuildRoadHandler?.Invoke(nameOfStructure);
    }

    private void OnBuildSingleStructureCallback(string nameOfStructure)
    {
        PrepareUIForBuilding();
        OnBuildSingleStructureHandler?.Invoke(nameOfStructure);
    }

    private void PrepareUIForBuilding()
    {
        cancleActionPanel.SetActive(true);
        OnCloseMenuHandler();
    }

    private void CreateButtonsInPanel(Transform panelTransform, List<string> dataToShow, Action<string> callback)
    {
        if (dataToShow.Count > panelTransform.childCount)
        {
            int quantityDifference = dataToShow.Count - panelTransform.childCount;
            for (int i = 0; i < quantityDifference; i++)
            {
                Instantiate(buildButtonPrefab, panelTransform);
            }
        }
        for (int i = 0; i < panelTransform.childCount; i++)
        {
            var button = panelTransform.GetChild(i).GetComponent<Button>();
            if (button != null)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = dataToShow[i];
                button.onClick.AddListener(() => callback(button.GetComponentInChildren<TextMeshProUGUI>().text));
            }
        }
    }

    private void OnBuildAreaCallback(string nameOfStructure)
    {
        PrepareUIForBuilding();
        OnBuildAreaHandler?.Invoke(nameOfStructure);
    }

    public void SetMoneyValue(int money)
    {
        moneyValue.text = money + "";
    }

    private void Update()
    {
        
    }

    public void DisplayBasicStructureInfo(StructureBaseSO data)
    {
        structurePanelHelper.DisplayBasicStructureInfo(data);
    }

    public void DisplayZoneStructureInfo(ZoneStructureSO data)
    {
        structurePanelHelper.DisplayZoneStructureInfo(data);
    }

    public void DisplayFacilitStructureInfo(SingleFacilitySO data)
    {
        structurePanelHelper.DisplayFacilityStructureInfo(data);
    }

    private void OnCancleActionCallback()
    {
        cancleActionPanel.SetActive(false);
        OnCancleActionHandler?.Invoke();
    }

    public void AddListenerOnBuildAreaEvent(Action<string> listener)
    {
        OnBuildAreaHandler += listener;
    }

    public void RemoveListenerOnBuildAreaEvent(Action<string> listener)
    {
        OnBuildAreaHandler -= listener;
    }
    public void AddListenerOnCancleActionEvent(Action listener)
    {
        OnCancleActionHandler += listener;
    }

    public void RemoveListenerOnCancleActionEvent(Action listener)
    {
        OnCancleActionHandler -= listener;
    }

    public void AddListenerOnDemolishActionEvent(Action listener)
    {
        OnDemolishActionHandler += listener;
    }

    public void RemoveListenerOnDemolishActionEvent(Action listener)
    {
        OnDemolishActionHandler -= listener;
    }
    public void AddListenerOnBuildSingleStructureEvent(Action<string> listener)
    {
        OnBuildSingleStructureHandler += listener;
    }

    public void RemoveListenerOnBuildSingleStructureEvent(Action<string> listener)
    {
        OnBuildSingleStructureHandler -= listener;
    }

    public void AddListenerOnBuildRoadEvent(Action<string> listener)
    {
        OnBuildRoadHandler += listener;
    }

    public void RemoveListenerOnBuildRoadEvent(Action<string> listener)
    {
        OnBuildRoadHandler -= listener;
    }

    public void AddListenerOnConfirmActionEvent(Action listener)
    {
        OnConfirmActionHandler += listener;
    }

    public void RemoveListenerOnConfirmActionEvent(Action listener)
    {
        OnConfirmActionHandler -= listener;
    }
}
