using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectCore.Features 
{

    using Components; using Modules; using Systems; using Features; using Markers;
    using Food.Components; using Food.Modules; using Food.Systems; using Food.Markers;
    
    namespace Food.Components {}
    namespace Food.Modules {}
    namespace Food.Systems {}
    namespace Food.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class FoodFeature : Feature
    {
        public ViewId AppleViewId { get; private set; }
        public ViewId BananaViewId { get; private set; }

        public int CurrentEatenApplesForBanana;
        
        [SerializeField] private MonoBehaviourView _appleView;
        [SerializeField] private MonoBehaviourView _bananaView;
        
        protected override void OnConstruct() 
        {
            AddSystem<FoodSpawnSystem>();
            AddSystem<FoodPositionChangeSystem>();
            
            AppleViewId = world.RegisterViewSource(_appleView);
            BananaViewId = world.RegisterViewSource(_bananaView);
        }

        protected override void OnConstructLate()
        {
            var appleEntity = world.AddEntity();

            appleEntity.Set(new FoodSpawn()
            {
                FoodType = FoodType.Apple
            });

            appleEntity.Set<FoodSpawnTag>();
            
            appleEntity.Set<FoodChangePosition>();
        }

        protected override void OnDeconstruct() 
        {
            
        }
    }

}