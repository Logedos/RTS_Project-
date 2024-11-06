using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StorageStruct
{
    public List<IResources> resourcesList;
    public int maxAmount;
    public int maxPopulation;
};

