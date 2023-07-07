using System.Collections.Generic;
using System.Linq;
using ME.ECS;
using OOP.Services.Fabric.UIFactory;
using OOP.Services.Locator;
using OOP.Services.WebSocketsService;
using ProjectCore.Features.Food.Components;
using ProjectCore.Features.Map.Components;
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
        private FoodFeature _foodFeature;

        private int3 _moveOffset;

        private IWebSocketsService _webSocketsService;
        private IUIFactory _uiFactory;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _snakeFeature);
            
            _mapFeature = world.GetFeature<MapFeature>();
            _foodFeature = world.GetFeature<FoodFeature>();

            _webSocketsService = ServiceLocator.Container.Single<IWebSocketsService>();
            _uiFactory = ServiceLocator.Container.Single<IUIFactory>();
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

            ref var snakeBody = ref entity.Get<SnakeBody>();
            
            var snakeSpeed = 1 / _snakeFeature.SnakeConfig.Read<SnakeMovementSpeed>().Value;
            var snakeDirection = entity.Read<SnakeMoveDirection>().currentDirection;

            var mapProperties = _mapFeature.MapConfig.Read<MapProperties>();

            timer += deltaTime;

            if (!CanMakeStep(timer, snakeSpeed))
            {
                return;
            }

            ref var currentSnakePos = ref entity.Get<SnakeHead>().PositionInMatrix;

            if (CheckForDeath(entity, currentSnakePos))
            {
                return;
            }

            _snakeFeature.CurrentHeadPosition = currentSnakePos;

            _snakeFeature.SnakePositionsForMovement.Insert(0, currentSnakePos);
            
            SetMoveOffset(snakeDirection);

            currentSnakePos += _moveOffset;

            currentSnakePos = CheckForOutOfBorders(currentSnakePos, mapProperties);

            TryEatFood(currentSnakePos,ref snakeBody);

            if (_snakeFeature.SnakePositionsForMovement.Count >= snakeBody.Length + 1)
            {
                _snakeFeature.SnakePositionsForMovement.RemoveAt(_snakeFeature.SnakePositionsForMovement.Count - 1);
            }

            entity.SetPosition(currentSnakePos);

            SpawnBodyParts();

            timer = 0;
        }

        private bool CheckForDeath(Entity entity, int3 currentSnakePos)
        {
            if (!_snakeFeature.SnakePositionsForMovement.Any(position =>
                    position.x == currentSnakePos.x && position.z == currentSnakePos.z))
            {
                return false;
            }
            
            entity.Remove<SnakeMovementSpeed>();
                    
            _webSocketsService.PostEndGame();

            return true;
        }

        private void SpawnBodyParts()
        {
            for (int i = 0; i < _snakeFeature.SnakePositionsForMovement.Count; i++)
            {
                var spawnBodyMarker = world.AddEntity();

                spawnBodyMarker.Set(new SnakePartSpawn()
                {
                    BodySpawnPosition = _snakeFeature.SnakePositionsForMovement[i]
                });
            }
        }

        private static int3 CheckForOutOfBorders(int3 currentSnakePos, MapProperties mapProperties)
        {
            if (currentSnakePos.x < 0)
            {
                currentSnakePos.x = mapProperties.HorizontalCellAmount - 1;
            }
            else if (currentSnakePos.x > mapProperties.HorizontalCellAmount - 1)
            {
                currentSnakePos.x = 0;
            }
            else if (currentSnakePos.z < 0)
            {
                currentSnakePos.z = mapProperties.VerticalCellAmount - 1;
            }
            else if (currentSnakePos.z > mapProperties.VerticalCellAmount - 1)
            {
                currentSnakePos.z = 0;
            }

            return currentSnakePos;
        }

        private void TryEatFood(int3 currentSnakePos, ref SnakeBody snakeBody)
        {
            ref var currentCell = ref _mapFeature.MapMatrix[currentSnakePos.x, currentSnakePos.z];
            
            if (currentCell.Food == null)
            {
                return;
            }

            if (currentCell.Food.Value.Get<FoodSpawn>().FoodType == FoodType.Apple)
            {
                snakeBody.Length += 1;
                snakeBody.ApplesEaten += 1;
                
                _foodFeature.CurrentEatenApplesForBanana++;

                currentCell.Food.Value.Set<FoodChangePosition>();

                if (_foodFeature.CurrentEatenApplesForBanana >= 5)
                {
                    AddBananaToMap();
                }
                
                _webSocketsService.PostSnakeCollectedApple(snakeBody.ApplesEaten, snakeBody.Length + 1);
            }
            else if (currentCell.Food.Value.Get<FoodSpawn>().FoodType == FoodType.Banana)
            {
                snakeBody.Length += 2;
                currentCell.Food.Value.Set<DestroyImmediately>();
            }
            
            _uiFactory.GameplayScreenComponent.UpdateApplesText(snakeBody.ApplesEaten);
            _uiFactory.GameplayScreenComponent.UpdateLenghtText(snakeBody.Length + 1);
            
            currentCell.Food = null;
        }

        private void AddBananaToMap()
        {
            var bananaEntity = world.AddEntity();

            bananaEntity.Set(new FoodSpawn()
            {
                FoodType = FoodType.Banana
            });

            bananaEntity.Set<FoodSpawnTag>();

            bananaEntity.Set<Timer>();

            bananaEntity.Set(new DestroyAfterTime()
            {
                TimeToDestroy = 5f
            });

            bananaEntity.Set<FoodChangePosition>();

            _foodFeature.CurrentEatenApplesForBanana = 0;
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