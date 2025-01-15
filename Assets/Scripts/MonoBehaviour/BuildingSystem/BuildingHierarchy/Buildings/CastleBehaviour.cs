using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBehaviour : EconomicBuilding
{
    [SerializeField] private int currentHp;
    [SerializeField] private int maxBuildingHP;
    [SerializeField] private int level;
    [SerializeField] private string name;
    
    [SerializeField] private int populationToIncreaseAmount;
    
    public override void PerformAction()
    {
        IncreasePopulation(populationToIncreaseAmount);
    }

    private void IncreasePopulation(int amount) => JsonSaver.SaveResources(ResType.Population, amount);

    private void Awake()
    {
        InitStatFields();
    }
    
    private void InitStatFields()
    {
        CurrentBuildingHP = currentHp;
        MaxBuildingHP = maxBuildingHP;
        Level = level;
        Name = name;
    }
}
