using ME.ECS;
using OOP.Services.Input;
using OOP.Services.Locator;
using UnityEngine;

namespace ProjectCore.Features 
{

    using Components; using Modules; using Systems; using Features; using Markers;
    using Input.Components; using Input.Modules; using Input.Systems; using Input.Markers;
    
    namespace Input.Components {}
    namespace Input.Modules {}
    namespace Input.Systems {}
    namespace Input.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class InputFeature : Feature
    {
        private PlayerInputActionReader _playerInputActionReader;
        
        protected override void OnConstruct()
        {
            _playerInputActionReader = ServiceLocator.Container.Single<PlayerInputActionReader>();
        }

        protected override void OnDeconstruct() 
        {
            
        }

    }

}