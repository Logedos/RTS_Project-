using Leopotam.Ecs;
using UnityEngine;

public sealed class EntitiesInitializationSystem : IEcsRunSystem
{
    private readonly EcsFilter<EntityReferenceComponent, OnInitializated> _entitiesFilter;

    public void Run()
    {
        foreach (var unit in _entitiesFilter)
        {
            ref EcsEntity entity = ref _entitiesFilter.GetEntity(unit);
            ref EntityReferenceComponent referenceComponent = ref _entitiesFilter.Get1(unit);

            referenceComponent.entityReference.entity = entity;
            entity.Del<OnInitializated>();
        }
    }
}