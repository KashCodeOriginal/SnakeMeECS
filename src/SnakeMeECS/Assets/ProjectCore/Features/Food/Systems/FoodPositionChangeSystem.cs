﻿using ME.ECS;
using ProjectCore.Features.Map.Components;
using UnityEngine;

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
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this._foodFeature);

            _mapFeature = world.GetFeature<MapFeature>();
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-FoodPositionChangeSystem")
                .With<FoodChangePositionTag>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var mapMatrixConfig = _mapFeature.MapConfig.Get<MapProperties>();
            
            int randomXMatrixPosition;
            int randomZMatrixPosition;
            
            do
            {
                randomXMatrixPosition = Random.Range(0,mapMatrixConfig.HorizontalCellAmount);
                randomZMatrixPosition = Random.Range(0,mapMatrixConfig.VerticalCellAmount);
            }
            while (_mapFeature.MapMatrix[randomXMatrixPosition, randomZMatrixPosition].SnakePart != null);

            var positionInMatrix = _mapFeature.MapMatrix[randomXMatrixPosition, randomZMatrixPosition].Position;
            
            entity.SetPosition(positionInMatrix);

            _mapFeature.MapMatrix[randomXMatrixPosition, randomZMatrixPosition].Food = entity;

            entity.Remove<FoodChangePositionTag>();
        }
    }
}