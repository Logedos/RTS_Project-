using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class EconomicBuilding : MonoBehaviour, IBuilding
{
    public int CurrentBuildingHP { get; set; }
    public int MaxBuildingHP { get; set; }
    public int Level { get; set; }
    public string Name { get; set; }

    public virtual void PerformAction() { }
}