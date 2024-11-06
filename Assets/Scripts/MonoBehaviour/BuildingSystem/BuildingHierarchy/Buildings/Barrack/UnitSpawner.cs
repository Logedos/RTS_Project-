using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class UnitSpawner : MonoBehaviour
{
    public static void SpawnUnit(GameObject unitToSpawn, Vector3 spawnPoint, Vector3 startDestinationPosition)
    {
        SetStartDestination(Spawn(unitToSpawn, spawnPoint), startDestinationPosition);
        GetUnitSelections().unitList.Add(unitToSpawn);
    }

    private static GameObject Spawn(GameObject unitToSpawn, Vector3 spawnPoint) =>
        Instantiate(unitToSpawn, spawnPoint, Quaternion.identity, GetUnitsObjectPool());

    private static void SetStartDestination(GameObject unit, Vector3 startDestinationPosition)
    {
        if (unit.TryGetComponent(out NavMeshAgent agent))
        {
            agent.SetDestination(startDestinationPosition);
        }
    }

    private static Transform GetUnitsObjectPool() => GameObject.Find("Units").transform;
    private static UnitSelections GetUnitSelections() => GameObject.Find("Unit Select").GetComponent<UnitSelections>();
}
