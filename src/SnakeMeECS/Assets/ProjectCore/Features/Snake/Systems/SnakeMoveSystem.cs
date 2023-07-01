using ME.ECS;
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
        private InputFeature _inputFeature;
        private MapFeature _mapFeature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _snakeFeature);
            
            _inputFeature = world.GetFeature<InputFeature>();
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
            var snakeSpeed = _snakeFeature.SnakeConfig.Read<SnakeMovementSpeed>().Value;

            snakeSpeed = 1 / snakeSpeed;

            timer += deltaTime;

            if (timer >= snakeSpeed)
            {
                var currentSnakePos = entity.Get<SnakePart>().PositionInMatrix;
                currentSnakePos.x += 1;
            
                entity.SetPosition(currentSnakePos);

                entity.Get<SnakePart>().PositionInMatrix = currentSnakePos;

                timer = 0;
            }

            
        }
    }
    
}