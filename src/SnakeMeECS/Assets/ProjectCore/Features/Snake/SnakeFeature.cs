using System.Collections.Generic;
using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using ProjectCore.Components;
using ProjectCore.Features.Map.Components;
using ProjectCore.Features.Snake.Components;
using ProjectCore.Features.Snake.Systems;
using Unity.Mathematics;
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
        public ViewId SnakeHeadViewId { get; private set; }
        public ViewId SnakePartViewId { get; private set; }

        public List<int3> SnakePositions = new();

        [field: SerializeField] public DataConfig SnakeConfig { get; private set; }
        [SerializeField] private MonoBehaviourViewBase _snakeHeadView;
        [SerializeField] private MonoBehaviourViewBase _snakePartView;

        protected override void OnConstruct()
        {
            AddSystem<SnakeHeadSpawnSystem>();
            AddSystem<SnakeHeadMoveSystem>();
            AddSystem<SnakeDirectionChangeSystem>();
            AddSystem<SnakePartCreationSystem>();
            AddSystem<SnakePartDestroySystem>();

            SnakeHeadViewId = world.RegisterViewSource(_snakeHeadView);
            SnakePartViewId = world.RegisterViewSource(_snakePartView);
        }

        protected override void OnConstructLate()
        {
            var snakeEntity = world.AddEntity();

            var snakeMoveSpeed = SnakeConfig.Get<SnakeMovementSpeed>().Value;

            snakeEntity.Set<SnakeInitializer>();

            snakeEntity.Set<SnakeHead>();

            snakeEntity.Set<Timer>();

            snakeEntity.Set(new SnakeMovementSpeed()
            {
                Value = snakeMoveSpeed
            });

            snakeEntity.Set(new SnakeMoveDirection()
            {
                currentDirection = Direction.Up
            });

            snakeEntity.Set(new SnakeBody()
            {
                Length = 2
            });
        }

        protected override void OnDeconstruct() 
        {
            
        }

    }

}