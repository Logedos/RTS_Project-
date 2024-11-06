using UnityEngine;
using UnityEngine.AI;
using Leopotam.Ecs;

public class UnitsFormationSystem : IEcsRunSystem
{
    private readonly EcsFilter<UnitTag, OnNavigated, UnitNavMeshAgentComponent, UnitFormationComponent, UnitAnimationComponent> navigatedUnitsFilter;
    private UnitSelections UnitSelections => UnitSelections.unitSelections;
    public static int xRepetitions;
    public static int zRepetitions;

    public void Run()
    {
        foreach (var unit in navigatedUnitsFilter)
        {
            ref var entity = ref navigatedUnitsFilter.GetEntity(unit);
            
            ref var maxDistance = ref navigatedUnitsFilter.Get3(unit).maxDistance;
            ref var destination = ref navigatedUnitsFilter.Get3(unit).destination;
            ref var agent = ref navigatedUnitsFilter.Get3(unit).unitAgent;
            ref var formatedEntities = ref navigatedUnitsFilter.Get4(unit).formatedEntities;
            ref var cellSize = ref navigatedUnitsFilter.Get4(unit).sellSize;
            ref var animator = ref navigatedUnitsFilter.Get5(unit).UnitAnimator;
            
            if (formatedEntities.Contains(agent))
                continue;

            formatedEntities.Add(agent);
            agent.SetDestination(FormatePosition(destination, maxDistance, formatedEntities.Count, cellSize));
            animator.SetBool("Movement", true);
            entity.Get<OnDestinated>();
            entity.Del<OnNavigated>();
        }
    }
    
    private Vector3 FormatePosition(Vector3 destination, int maxDistance, int formatedEntitiesAmount, float cellSize)
    {
        float selectedUnitsAmount = UnitSelections.unitsSelected.Count;
        
        const float unitsRawDividend = 2;
        const float xOffset = 1.75f;
        
        float x = selectedUnitsAmount / unitsRawDividend;
        
        float leftSidedUnitsAmount = x / 2;
        
        Vector3 initialFormationPosition =
            new Vector3(destination.x - leftSidedUnitsAmount * xOffset, destination.y, destination.z);


        if (xRepetitions >= x)
        {
            xRepetitions = 0;
            zRepetitions++;
        }

        xRepetitions++;

        Vector3 formationPosition = new Vector3(initialFormationPosition.x + cellSize * xRepetitions,
            initialFormationPosition.y, initialFormationPosition.z + cellSize * zRepetitions);

        if (!NavMesh.SamplePosition(formationPosition, out NavMeshHit hit, maxDistance, NavMesh.AllAreas))
            return formationPosition;
        
        return hit.position;
    }
}
