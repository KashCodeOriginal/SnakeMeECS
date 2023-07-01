using ME.ECS;
using Unity.Mathematics;

namespace ProjectCore.Features.Snake.Components 
{
    public struct SnakePart : IComponent
    {
        public int3 PositionInMatrix;
        public bool IsHead;
    }
}