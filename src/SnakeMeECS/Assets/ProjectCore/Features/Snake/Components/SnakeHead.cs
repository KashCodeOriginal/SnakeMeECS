using ME.ECS;
using Unity.Mathematics;

namespace ProjectCore.Features.Snake.Components 
{
    public struct SnakeHead : IComponent
    {
        public int3 PositionInMatrix;
    }
}