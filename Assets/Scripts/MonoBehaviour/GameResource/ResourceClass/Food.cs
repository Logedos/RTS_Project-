using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Food : IResources
{
    public Food()
    {
        resType = ResType.Food;
    }
}
