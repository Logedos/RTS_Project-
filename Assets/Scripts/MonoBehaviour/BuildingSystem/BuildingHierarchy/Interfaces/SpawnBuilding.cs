using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnBuilding : MonoBehaviour, IBuilding
{
    public int CurrentBuildingHP { get; set; }
    public int MaxBuildingHP { get; set; }
    public int Level { get; set; }
    public string Name { get; set; }
}
