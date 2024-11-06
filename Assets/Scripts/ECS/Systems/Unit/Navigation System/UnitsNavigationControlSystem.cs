using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Leopotam.Ecs;
using Vector3 = UnityEngine.Vector3;

public class UnitsNavigationControlSystem : IEcsRunSystem
{
    private readonly EcsFilter<UnitTag, UnitNavMeshAgentComponent, UnitFormationComponent, UnitTransformComponent> movingUnitsFilter;

    public void Run()
    {
        foreach (var unit in movingUnitsFilter)
        {
            ref var entity = ref movingUnitsFilter.GetEntity(unit);
            
            ref var agent = ref movingUnitsFilter.Get2(unit).unitAgent;
            ref var destination = ref movingUnitsFilter.Get2(unit).destination;
            //ref var formationPositions = ref movingUnitsFilter.Get3(unit).formatedPositions;
            ref var transform = ref movingUnitsFilter.Get4(unit).unitTransform;

            if (!agent.hasPath)
                continue;
            
            float distanceToDestination = Vector3.Distance(transform.position, destination);

            if (distanceToDestination > 40)
                continue;

            entity.Get<OnFormated>();
        }
    }
}