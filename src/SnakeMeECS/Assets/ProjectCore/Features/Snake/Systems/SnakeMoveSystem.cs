using ME.ECS;
using Unity.Mathematics;
using UnityEngine;

namespace ProjectCore.Features.Snake.Systems 
{
#pragma warning disable
    using ProjectCore.Components; using ProjectCore.Modules; using ProjectCore.Systems; using ProjectCore.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class SnakeMoveSystem : ISystemFilter
    {
        private SnakeFeature _snakeFeature;
        private MapFeature _mapFeature;

        private int3 _moveOffset;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _snakeFeature);
            
            _mapFeature = world.GetFeature<MapFeature>();
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => true;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-SnakeMoveSystem").
                With<SnakeMovementSpeed>().
                With<Timer>().
                Without<SnakeInitializer>().
                Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var timer = ref entity.Get<Timer>().Value;
            
            var snakeSpeed = 1 / _snakeFeature.SnakeConfig.Read<SnakeMovementSpeed>().Value;
            var snakeDirection = entity.Read<SnakeMoveDirection>().currentDirection;

            timer += deltaTime;

            if (!CanMakeStep(timer, snakeSpeed))
            {
                return;
            }
            
            ref var currentSnakePos = ref entity.Get<SnakePart>().PositionInMatrix;

            SetMoveOffset(snakeDirection);

            currentSnakePos += _moveOffset;

            entity.SetPosition(currentSnakePos);

            timer = 0;
        }

        private static bool CanMakeStep(float timer, float snakeSpeed)
        {
            if (timer < snakeSpeed)
            {
                return false;
            }

            return true;
        }

        private void SetMoveOffset(Direction snakeDirection)
        {
            if (snakeDirection == Direction.Up)
            {
                if (_moveOffset.z != -1)
                {
                    _moveOffset = new int3(0, 0, 1);
                }
            }
            else if (snakeDirection == Direction.Down)
            {
                if (_moveOffset.z != 1)
                {
                    _moveOffset = new int3(0, 0, -1);
                }
            }
            else if (snakeDirection == Direction.Right)
            {
                if (_moveOffset.x != -1)
                {
                    _moveOffset = new int3(1, 0, 0);
                }
            }
            else if (snakeDirection == Direction.Left)
            {
                if (_moveOffset.x != 1)
                {
                    _moveOffset = new int3(-1, 0, 0);
                }
            }
        }
    }
    
}