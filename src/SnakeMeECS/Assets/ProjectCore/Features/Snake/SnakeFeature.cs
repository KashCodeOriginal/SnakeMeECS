using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using ProjectCore.Features.Snake.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectCore.Features 
{
    namespace Snake.Components {}
    namespace Snake.Modules {}
    namespace Snake.Systems {}
    namespace Snake.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class SnakeFeature : Feature
    {
        [SerializeField] private DataConfig _snakeConfig;
        [SerializeField] private MonoBehaviourViewBase _snakeView;
        
        protected override void OnConstruct()
        {
            var snakeEntity = world.AddEntity();

            snakeEntity.Set(new SnakeInitializer()
            {
                StartPosition = _snakeConfig.Get<SnakeInitializer>().StartPosition
            });
        }

        protected override void OnDeconstruct() 
        {
            
        }

    }

}