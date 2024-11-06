using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbarBehaviour : EconomicBuilding
{
    [SerializeField] private int currentHp;
    [SerializeField] private int maxBuildingHP;
    [SerializeField] private int level;
    [SerializeField] private string name;
    
    [SerializeField] private int amountToExpand;
    
    public override void PerformAction()
    {
        IncreaseResourceMaxAmount(amountToExpand);
    }

    private void IncreaseResourceMaxAmount(int amount)
    {
        JsonSaver.ExpandMaxAmout(amount);
    }

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
