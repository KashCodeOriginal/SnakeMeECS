using System.Collections.Generic;
using ME.ECS;
using ProjectCore.Features.Food.Components;
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
    public sealed class SnakeHeadMoveSystem : ISystemFilter
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

            ref var  snakeBody = ref entity.Get<SnakeBody>();
            
            var snakeSpeed = 1 / _snakeFeature.SnakeConfig.Read<SnakeMovementSpeed>().Value;
            var snakeDirection = entity.Read<SnakeMoveDirection>().currentDirection;

            timer += deltaTime;

            if (!CanMakeStep(timer, snakeSpeed))
            {
                return;
            }

            ref var currentSnakePos = ref entity.Get<SnakeHead>().PositionInMatrix;
            
            _snakeFeature.SnakePositions.Insert(0, currentSnakePos);
  
            SetMoveOffset(snakeDirection);

            currentSnakePos += _moveOffset;

            if (TryEatFood(currentSnakePos))
            {
                snakeBody.Length++;
            }
            
            if (_snakeFeature.SnakePositions.Count >= snakeBody.Length + 1)
            {
                _snakeFeature.SnakePositions.RemoveAt(_snakeFeature.SnakePositions.Count - 1);
            }

            entity.SetPosition(currentSnakePos);

            for (int i = 0; i < _snakeFeature.SnakePositions.Count; i++)
            {
                var spawnBodyMarker = world.AddEntity();
                
                spawnBodyMarker.Set(new SnakePartSpawn()
                {
                    BodySpawnPosition = _snakeFeature.SnakePositions[i]
                });
            }

            timer = 0;
        }

        private bool TryEatFood(int3 currentSnakePos)
        {
            if (_mapFeature.MapMatrix[currentSnakePos.x, currentSnakePos.z].Food != null)
            {
                _mapFeature.MapMatrix[currentSnakePos.x, currentSnakePos.z].Food.Value.Set<FoodChangePositionTag>();
                return true;
            }

            return false;
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