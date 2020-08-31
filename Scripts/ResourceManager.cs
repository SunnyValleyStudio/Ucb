using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour, IResourceManager
{
    [SerializeField]
    private int startMoneyAmount = 5000;
    [SerializeField]
    private int demolitionPrice = 20;
    [SerializeField]
    private float moneyCalculationInterval = 2;
    MoneyHelper moneyHelper;
    private BuildingManager buildingManger;
    public UiController uiController;

    public int StartMoneyAmount { get => startMoneyAmount;}
    public float MoneyCalculationInterval { get => moneyCalculationInterval;}

    public int DemolitionPrice => demolitionPrice;

    // Start is called before the first frame update
    void Start()
    {
        moneyHelper = new MoneyHelper(startMoneyAmount);
        UpdateMoneyValueUI();
    }

    public void PrepareResourceManager(BuildingManager buildingManager)
    {
        this.buildingManger = buildingManager;
        InvokeRepeating("CalculateTownIncome",0,MoneyCalculationInterval);
    }

    public bool SpendMoney(int amount)
    {
        if (CanIBuyIt(amount))
        {
            try
            {
                moneyHelper.ReduceMoney(amount);
                UpdateMoneyValueUI();
                return true;
            }
            catch (MoneyException)
            {

                ReloadGame();
            }
        }
        return false;
    }

    private void ReloadGame()
    {
        Debug.Log("End the game");
    }

    public bool CanIBuyIt(int amount)
    {
        return moneyHelper.Money >= amount;
    }

    public void CalculateTownIncome()
    {
        try
        {
            moneyHelper.CalculateMoney(buildingManger.GetAllStructures());
            UpdateMoneyValueUI();
        }
        catch (MoneyException)
        {
            ReloadGame();
        }
    }

    private void OnDisable()
    {
        CancelInvoke();  
    }

    public void AddMoney(int amount)
    {
        moneyHelper.AddMoney(amount);
        UpdateMoneyValueUI();
    }

    private void UpdateMoneyValueUI()
    {
        uiController.SetMoneyValue(moneyHelper.Money);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int HowManyStructuresCanIPlace(int placementCost, int numberOfStructures)
    {
        int amount = (int)(moneyHelper.Money / placementCost);
        return amount > numberOfStructures ? numberOfStructures : amount;
    }
}
