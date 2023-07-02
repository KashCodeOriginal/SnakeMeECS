using ME.ECS;
using ProjectCore.Features.Map.Components;
using Unity.Mathematics;
using UnityEngine;

namespace ProjectCore.Features.Snake.Systems {

    #pragma warning disable
    using ProjectCore.Components; using ProjectCore.Modules; using ProjectCore.Systems; using ProjectCore.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class SnakeHeadSpawnSystem  : ISystemFilter {
        
        private SnakeFeature _snakeFeature;
        private MapFeature _mapFeature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _snakeFeature);
            _mapFeature = world.GetFeature<MapFeature>();
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-SnakeSpawnSystem")
                .With<SnakeInitializer>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var matrixPosition = _snakeFeature.SnakeConfig.Get<SnakeInitializer>().SpawnSnakeMatrixPosition;

            var snakePositionFromMatrix = 
                _mapFeature.MapMatrix[matrixPosition.x, matrixPosition.y].Position;

            var targetSnakePosition = new Vector3(snakePositionFromMatrix.x, snakePositionFromMatrix.y + 1,
                snakePositionFromMatrix.z);

            _mapFeature.MapMatrix[matrixPosition.x, matrixPosition.y].SnakePart = entity;

            entity.SetPosition(targetSnakePosition);

            entity.Get<SnakeHead>().PositionInMatrix = new int3(targetSnakePosition);
            
            world.InstantiateView(_snakeFeature.SnakeHeadViewId, entity);

            entity.Remove<SnakeInitializer>();
        }
    
    }
    
}