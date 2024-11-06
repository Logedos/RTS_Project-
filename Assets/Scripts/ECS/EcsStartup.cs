using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;

public class EcsStartup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;
    
    private void Awake()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _systems.ConvertScene();

        AddInjections();
        AddOneFrames();
        AddSystems();
    }

    private void Start()
    {
        _systems.Init();
    }

    private void Update()
    {
        _systems.Run();
    }

    private void AddInjections()
    {
        
    }

    private void AddSystems()
    {
        _systems
            .Add(new EntitiesInitializationSystem())
            .Add(new UnitsSetPositionSystem())
            .Add(new UnitsFormationSystem())
            .Add(new AnimationControlSystem());
    }

    private void AddOneFrames()
    {
        
    }

    private void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
        }
        
        if (_world != null)
        {
            _world.Destroy();
            _world = null;
        }
    }
}