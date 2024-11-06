using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public struct UnitNavMeshAgentComponent
{
    public int maxDistance;
    public NavMeshAgent unitAgent;
    [HideInInspector] public Vector3 destination;
}

