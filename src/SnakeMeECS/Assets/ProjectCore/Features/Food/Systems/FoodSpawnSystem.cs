using ME.ECS;
using ProjectCore.Features.Map.Components;
using Unity.Mathematics;
using UnityEngine;
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
    public sealed class FoodSpawnSystem : ISystemFilter 
    {
        private FoodFeature _foodFeature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out this._foodFeature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-FoodSpawnSystem")
                .With<FoodInitializer>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            world.InstantiateView(_foodFeature.AppleViewId, entity);
            
            entity.Remove<FoodInitializer>();
        }
    }
    
}