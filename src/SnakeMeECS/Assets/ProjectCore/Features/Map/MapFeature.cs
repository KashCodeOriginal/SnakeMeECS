using ME.ECS;
using ME.ECS.DataConfigs;
using ME.ECS.Views.Providers;
using UnityEngine;

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
        public ViewId ViewId { get; private set; }

        public CellInMatrix[,] MapMatrix;
        
        [SerializeField] private DataConfig _mapConfig;
        [SerializeField] private MonoBehaviourViewBase _cellView;

        private Vector3 _centerPosition;
        
        protected override void OnConstruct()
        {
            AddSystem<CellSpawnSystem>();
            
            ViewId = world.RegisterViewSource(_cellView);
        }

        protected override void OnConstructLate()
        {
            var mapProps = _mapConfig.Get<MapProperties>();
            
            _centerPosition = new Vector3(-mapProps.HorizontalCellAmount / 2, mapProps.MapHeight,
                -mapProps.VerticalCellAmount / 2);

            MapMatrix = new CellInMatrix[mapProps.HorizontalCellAmount, mapProps.VerticalCellAmount];

            _centerPosition += mapProps.CenterPosition;
            
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
                        Position = targetSpawnPosition,
                        IsSnakeInCell = false
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
            return _centerPosition + 
                   Vector3.forward * column * distance + 
                   Vector3.right * row * distance;
        }

    }

}