using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum ResType{
Stone,
Wood,
Food,
Gold,
Population
}

[Serializable]
public class IResources
{
   public int currentAmount;
   public ResType resType;
   
}
