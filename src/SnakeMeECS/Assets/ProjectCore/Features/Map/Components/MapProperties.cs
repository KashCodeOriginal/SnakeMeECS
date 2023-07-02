using ME.ECS;
using UnityEngine;

namespace ProjectCore.Features.Map.Components
{
    public struct MapProperties : IComponent
    {
        public int HorizontalCellAmount;
        public int VerticalCellAmount;

        public float DistanceBetweenCells;
        
        public int MapHeight;
    }
}