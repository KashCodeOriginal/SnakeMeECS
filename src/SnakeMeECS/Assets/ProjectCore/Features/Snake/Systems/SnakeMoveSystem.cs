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
    public sealed class SnakeMoveSystem : ISystemFilter {
        
        private SnakeFeature _snake;
        private InputFeature _input;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _snake);
            
            _input = world.GetFeature<InputFeature>();
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-SnakeMoveSystem").
                With<SnakeMovementSpeed>().
                Without<SnakeInitializer>().
                Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var currentEntityPos = entity.GetPosition();

            var entityMoveSpeed = entity.Get<SnakeMovementSpeed>();

            if (_input.MoveDirection != Vector2.zero)
            {
                var currentPos = new Vector3(currentEntityPos.x, currentEntityPos.y, currentEntityPos.z);
                
                var moveDirection = new Vector3(_input.MoveDirection.x, 0, _input.MoveDirection.y).normalized;

                var newPos = currentPos + moveDirection;

                entity.SetPosition(Vector3.MoveTowards(currentPos, newPos, entityMoveSpeed.Value * deltaTime));
            }
        }
    
    }
    
}