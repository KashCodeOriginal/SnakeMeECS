using ME.ECS;

namespace ProjectCore.Features 
{

    using Components; using Modules; using Systems; using Features; using Markers;
    using Snake.Components; using Snake.Modules; using Snake.Systems; using Snake.Markers;
    
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

        protected override void OnConstruct() 
        {
            
        }

        protected override void OnDeconstruct() 
        {
            
        }

    }

}