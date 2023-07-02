using ME.ECS;
using ProjectCore.Features.Map.Components;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace ProjectCore.Features.Food.Systems {

    #pragma warning disable
    using ProjectCore.Components; using ProjectCore.Modules; using ProjectCore.Systems; using ProjectCore.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class FoodPositionChangeSystem : ISystemFilter {
        
        private FoodFeature _foodFeature;
        private MapFeature _mapFeature;
        private SnakeFeature _snakeFeature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _foodFeature);
            _mapFeature = world.GetFeature<MapFeature>();
            _snakeFeature = world.GetFeature<SnakeFeature>();
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            
            return Filter.Create("Filter-FoodPositionChangeSystem")
                .With<FoodChangePositionTag>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var mapMatrixConfig = _mapFeature.MapConfig.Get<MapProperties>();
            
            int randomXMatrixPosition;
            int randomZMatrixPosition;
            int yMatrixPosition;

            int3 newFoodPosition;
            
            do
            {
                randomXMatrixPosition = Random.Range(0,mapMatrixConfig.HorizontalCellAmount);
                randomZMatrixPosition = Random.Range(0,mapMatrixConfig.VerticalCellAmount);
                yMatrixPosition = mapMatrixConfig.MapHeight + 1;

                newFoodPosition = new int3(randomXMatrixPosition, yMatrixPosition, randomZMatrixPosition);
            }
            while (_snakeFeature.GetFullSnakeBody().IndexOf(newFoodPosition) != -1);

            var positionInMatrix = _mapFeature.MapMatrix[randomXMatrixPosition, randomZMatrixPosition].Position;
            
            entity.SetPosition(positionInMatrix);

            _mapFeature.MapMatrix[randomXMatrixPosition, randomZMatrixPosition].Food = entity;

            entity.Remove<FoodChangePositionTag>();
        }
    }
}