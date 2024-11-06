using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public struct UnitFormationComponent
{
    [HideInInspector] public List<NavMeshAgent> formatedEntities;
    public float sellSize;
}