using ME.ECS;
using UnityEngine;

namespace ProjectCore.Features.Map.Components
{
    public struct MapProperties : IComponent
    {
        public float HorizontalCellAmount;
        public float VerticalCellAmount;

        public float DistanceBetweenCells;
        
        public float MapHeight;

        public Vector3 CenterPosition;
    }
}