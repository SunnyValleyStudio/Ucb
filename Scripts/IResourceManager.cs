public interface IResourceManager
{
    float MoneyCalculationInterval { get;}
    int StartMoneyAmount { get;}
    int DemolitionPrice { get;}

    void AddMoney(int amount);
    void CalculateTownIncome();
    bool CanIBuyIt(int amount);
    bool SpendMoney(int amount);
    int HowManyStructuresCanIPlace(int placementCost, int count);

    void PrepareResourceManager(BuildingManager buildingManager);

    void AddToPopulation(int value);

    void ReducePopulation(int value);
}