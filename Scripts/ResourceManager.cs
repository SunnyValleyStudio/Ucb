using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour, IResourceManager
{
    private int startMoneyAmount = 5000;
    private float moneyCalculationInterval = 2;
    MoneyHelper moneyHelper;
    public BuildingManager buildingManger;
    public UiController uiController;

    public int StartMoneyAmount { get => startMoneyAmount; set => startMoneyAmount = value; }
    public float MoneyCalculationInterval { get => moneyCalculationInterval; set => moneyCalculationInterval = value; }

    // Start is called before the first frame update
    void Start()
    {
        moneyHelper = new MoneyHelper(startMoneyAmount);
        UpdateMoneyValueUI();
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
}
