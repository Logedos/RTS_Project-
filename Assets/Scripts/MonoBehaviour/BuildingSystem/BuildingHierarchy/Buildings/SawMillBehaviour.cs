using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMillBehaviour : MiningBuilding
{
    [SerializeField] private int currentHp;
    [SerializeField] private int maxBuildingHP;
    [SerializeField] private int level;
    [SerializeField] private string name;
    
    public override void Awake()
    {
        base.Awake();
        productionResType = ResType.Wood;
        
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