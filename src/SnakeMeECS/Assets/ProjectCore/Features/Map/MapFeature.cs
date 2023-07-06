using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectCore.Features 
{

    using Components; using Modules; using Systems; using Features; using Markers;
    using Map.Components; using Map.Modules; using Map.Systems; using Map.Markers;
    
    namespace Map.Components {}
    namespace Map.Modules {}
    namespace Map.Systems {}
    namespace Map.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class MapFeature : Feature 
    {
        public ViewId FirstCellViewId { get; private set; }

        public CellInMatrix[,] MapMatrix;
        
        [field: SerializeField] public DataConfig MapConfig { get; private set; }
        [SerializeField] private MonoBehaviourViewBase _firstCellView;

        private Vector3 _cornerPosition;
        
        protected override void OnConstruct()
        {
            AddSystem<CellSpawnSystem>();
            AddSystem<DestroyAfterTimeSystem>();
            AddSystem<DestroyImmediatelySystem>();
            
            FirstCellViewId = world.RegisterViewSource(_firstCellView);
        }

        protected override void OnConstructLate()
        {
            var mapProps = MapConfig.Get<MapProperties>();
            
            _cornerPosition = new Vector3(0, mapProps.MapHeight,
                0);

            MapMatrix = new CellInMatrix[mapProps.HorizontalCellAmount, mapProps.VerticalCellAmount];

            for (int x = 0; x < mapProps.HorizontalCellAmount; x++)
            {
                for (int y = 0; y < mapProps.VerticalCellAmount; y++)
                {
                    var spawnPosition = GetSpawnPosition(x, y, mapProps.DistanceBetweenCells);

                    var targetSpawnPosition = new Vector3(spawnPosition.x, 0, spawnPosition.z);

                    var cellEntity = world.AddEntity();

                    cellEntity.Set(new CellInitializer());

                    cellEntity.Set(new CellInMatrix()
                    {
                        Position = targetSpawnPosition
                    });

                    cellEntity.SetLocalPosition(targetSpawnPosition);

                    MapMatrix[x, y] = cellEntity.Get<CellInMatrix>();
                }
            }
        }

        protected override void OnDeconstruct() 
        {
            
        }
        
        Vector3 GetSpawnPosition(int row, int column, float distance)
        {
            return _cornerPosition + 
                   Vector3.forward * column * distance + 
                   Vector3.right * row * distance;
        }

    }

}