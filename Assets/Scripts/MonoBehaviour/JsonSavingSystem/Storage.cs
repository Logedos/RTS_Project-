using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Storage
{
    public List<IResources> resourcesList = new List<IResources>()
    {
        new Stone(),
        new Wood(),
        new Food(),
        new Gold(),
        new Population()
    };

    public int maxAmount = 0;
    public int maxPopulation = 0;

    public void ShowStorageInfo()
    {
        foreach (IResources resource in resourcesList)
        {
            Debug.Log(resource.resType + " Current amount: " + resource.currentAmount 
                      + " MaxAmount: " + resource.currentAmount);
        }
    }

    public ResType CheckResType(IResources resource)
    {
        if (resource is Stone)
            return ResType.Stone;
        else if(resource is Wood)
            return ResType.Wood;
        else if(resource is Food)
            return ResType.Food;
        else if(resource is Gold)
            return ResType.Gold;
        else if (resource is Population)
            return ResType.Population;
        
        Debug.Log(resource.GetType());
        
        return ResType.Stone;
    }
}
