using ME.ECS;
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
    public sealed class SnakeSpawnSystem : ISystemFilter {
        
        private SnakeFeature feature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out feature);
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
            world.InstantiateView(feature.ViewId, entity);

            entity.Remove<SnakeInitializer>();
        }
    
    }
    
}