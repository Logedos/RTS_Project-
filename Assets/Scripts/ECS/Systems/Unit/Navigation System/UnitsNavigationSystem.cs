using UnityEngine;
using Leopotam.Ecs;
using UnityEngine.AI;

public class UnitsNavigationSystem : IEcsRunSystem
{
    private readonly EcsFilter<OnSelected, OnNavigated, UnitNavMeshAgentComponent> selectedUnitsFilter;
    private int count;
    
    public void Run()
    {
        foreach (var unit in selectedUnitsFilter)
        {
            ref var agent = ref selectedUnitsFilter.Get3(unit).unitAgent;
            ref var unitDestination = ref selectedUnitsFilter.Get3(unit).destination;

            agent.SetDestination(unitDestination);
        }
    }
}

