using ME.ECS;

namespace ProjectCore.Systems {

    #pragma warning disable
    using ProjectCore.Components; using ProjectCore.Modules; using ProjectCore.Systems; using ProjectCore.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class DestroyAfterTimeSystem : ISystemFilter {
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {}
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-DestroyAfterTimeSystem")
                .With<DestroyAfterTime>()
                .With<Timer>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var timer = ref entity.Get<Timer>().Value;
            var destroyTime = entity.Read<DestroyAfterTime>().TimeToDestroy;

            timer += deltaTime;

            if (timer >= destroyTime)
            {
                world.RemoveEntity(entity);
            }
        }
    }
}