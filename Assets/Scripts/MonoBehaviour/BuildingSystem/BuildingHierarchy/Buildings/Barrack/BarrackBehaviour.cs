using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackBehaviour : EconomicBuilding
{
    [SerializeField] private int currentHp;
    [SerializeField] private int maxBuildingHP;
    [SerializeField] private int level;
    [SerializeField] private string name;
    
    public override void PerformAction()
    {
        base.PerformAction();
    }
}
