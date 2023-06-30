using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
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
        
        [SerializeField] private DataConfig _snakeConfig;
        [SerializeField] private MonoBehaviourViewBase _snakeView;
        
        protected override void OnConstruct()
        {
            AddSystem<SnakeSpawnSystem>();
            AddSystem<SnakeMoveSystem>();
            
            ViewId = world.RegisterViewSource(_snakeView);
        }

        protected override void OnConstructLate()
        {
            var snakeEntity = world.AddEntity();

            var startPosition = _snakeConfig.Get<SnakeInitializer>().StartPosition;
            var snakeMoveSpeed = _snakeConfig.Get<SnakeMovementSpeed>().Value;

            snakeEntity.Set(new SnakeInitializer()
            {
                StartPosition = startPosition
            });
            
            snakeEntity.SetLocalPosition(startPosition);

            snakeEntity.Set(new SnakeMovementSpeed()
            {
                Value = snakeMoveSpeed
            });
        }

        protected override void OnDeconstruct() 
        {
            
        }

    }

}