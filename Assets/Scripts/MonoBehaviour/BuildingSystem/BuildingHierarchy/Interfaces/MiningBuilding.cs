using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiningBuilding : MonoBehaviour, IBuilding
{
    private int currentBuildingHP;

    public int CurrentBuildingHP { get; set; }
    public int MaxBuildingHP { get; set; }
    public int Level { get; set; }
    public string Name { get; set; }
    
    public int resCountToAdd;
    public int maxWorker;
    public int currentPersonToWork;
    public ResType productionResType;

    private int finalProfit;

    public virtual void Awake()
    {
        CalculateProfit();
    }

    public virtual void AddRes() // добавляем ресурсы в джейсон
    {
        JsonSaver.SaveResources(productionResType, finalProfit);
        
       /* ChangeResValueManager.VisualizeResChanging(productionResType, finalProfit, 
            ChangeResValueManager.OperationType.Add);*/
    }

    public void TryAddNewWorker() // пробуем добавить работников
    {
        if (currentPersonToWork<maxWorker)
        {
            currentPersonToWork++;
            CalculateProfit();
        }
    }
    public void TryRemoveWorker() // пробуем убрать работников
    {
        if (currentPersonToWork > 1)
        {
            currentPersonToWork--;
            CalculateProfit();
        }
    }

    private void CalculateProfit() // считаем доход исходя из ревроначального значения
    {
        finalProfit = resCountToAdd * currentPersonToWork;
    }
}
