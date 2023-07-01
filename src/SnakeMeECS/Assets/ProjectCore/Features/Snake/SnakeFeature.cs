using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using ProjectCore.Components;
using ProjectCore.Features.Map.Components;
using ProjectCore.Features.Snake.Components;
using ProjectCore.Features.Snake.Systems;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectCore.Features 
{
    namespace Snake.Components {}
    namespace Snake.Modules {}
    namespace Snake.Systems {}
    namespace Snake.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class SnakeFeature : Feature
    {
        public ViewId ViewId { get; private set; }
        
        [field: SerializeField] public DataConfig SnakeConfig { get; private set; }
        [SerializeField] private MonoBehaviourViewBase _snakeView;

        protected override void OnConstruct()
        {
            AddSystem<SnakeSpawnSystem>();
            AddSystem<SnakeMoveSystem>();
            AddSystem<SnakeDirectionChangeSystem>();

            ViewId = world.RegisterViewSource(_snakeView);
        }

        protected override void OnConstructLate()
        {
            var snakeEntity = world.AddEntity();

            var snakeMoveSpeed = SnakeConfig.Get<SnakeMovementSpeed>().Value;

            snakeEntity.Set<SnakeInitializer>();

            snakeEntity.Set<SnakePart>();

            snakeEntity.Set<Timer>();

            snakeEntity.Set(new SnakeMovementSpeed()
            {
                Value = snakeMoveSpeed
            });

            snakeEntity.Set(new SnakeMoveDirection()
            {
                currentDirection = Direction.Up
            });
            
        }

        protected override void OnDeconstruct() 
        {
            
        }

    }

}