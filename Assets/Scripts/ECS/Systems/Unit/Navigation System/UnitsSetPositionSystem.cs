using UnityEngine;
using Leopotam.Ecs;
using UnityEngine.AI;

public class UnitsSetPositionSystem : IEcsInitSystem, IEcsRunSystem
{
    private readonly EcsFilter<OnSelected, UnitNavMeshAgentComponent, RaycastComponent, UnitFormationComponent> selectedUnitsFilter;
    private Camera camera;

    public void Init() => camera = Camera.main;

    public void Run()
    {
        if (!Input.GetMouseButtonDown(1)) return;

        foreach (var unit in selectedUnitsFilter) 
        {
            ref var entity = ref selectedUnitsFilter.GetEntity(unit);

            ref var agentDestination = ref selectedUnitsFilter.Get2(unit).destination;
            ref var maxDistance = ref selectedUnitsFilter.Get2(unit).maxDistance;
            ref var formatedEntities = ref selectedUnitsFilter.Get4(unit).formatedEntities;
            ref var groundMask = ref selectedUnitsFilter.Get3(unit).groundMask;

            if (!Input.GetMouseButtonDown(1)) return;
            
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundMask)) return;
                
            if (!NavMesh.SamplePosition(hitInfo.point, out NavMeshHit hit, maxDistance, NavMesh.AllAreas)) continue;
            
            agentDestination = hit.position;
            
            formatedEntities.Clear();
            UnitsFormationSystem.zRepetitions = 0;
            UnitsFormationSystem.xRepetitions = 0;
            
            entity.Get<OnNavigated>();
        }
    }
}