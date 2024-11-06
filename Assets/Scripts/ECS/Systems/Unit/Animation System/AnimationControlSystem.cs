using Leopotam.Ecs;
using UnityEngine;

public class AnimationControlSystem : IEcsRunSystem
{
    private readonly EcsFilter<UnitAnimationComponent, UnitNavMeshAgentComponent, UnitTransformComponent, OnDestinated> _unitFilter;
    
    public void Run()
    {
        foreach (var unit in _unitFilter)
        {
            ref var agent = ref _unitFilter.Get2(unit).unitAgent; 
            var destination = agent.destination;
            var stoppingDistance = agent.stoppingDistance;
            ref var transform = ref _unitFilter.Get3(unit).unitTransform;

            if (Vector3.Distance(transform.position, destination) > stoppingDistance)
                continue;
            
            ref var animator = ref _unitFilter.Get1(unit).UnitAnimator;
            ref var entity = ref _unitFilter.GetEntity(unit);
            
            animator.SetBool("Movement", false);
            entity.Del<OnDestinated>();
        }
    }
}
