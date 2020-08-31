using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationHelper
{
    private int population = 0;

    public int Population
    {
        get { return population; }
        private set { population = value; }
    }

    public void AddToPopulation(int value)
    {
        Population += value;
    }

    public void ReducePopulation(int value)
    {
        Population -= value;
    }
}
