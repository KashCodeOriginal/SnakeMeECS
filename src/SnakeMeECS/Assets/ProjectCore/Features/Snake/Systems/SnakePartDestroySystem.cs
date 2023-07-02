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
    public sealed class SnakePartDestroySystem : ISystemFilter 
    {
        
        private SnakeFeature _snakeFeature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _snakeFeature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-SnakePartDestroy")
                .With<Timer>()
                .With<SnakePartDestroy>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var timer = ref entity.Get<Timer>().Value;

            timer += deltaTime;

            var destroyTime = 1 / _snakeFeature.SnakeConfig.Get<SnakeMovementSpeed>().Value;

            if (timer >= destroyTime)
            {
                world.RemoveEntity(entity);

                timer = 0;
            }
        }
    
    }
    
}