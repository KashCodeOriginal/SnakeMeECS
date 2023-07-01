using ME.ECS;
using UnityEngine.Serialization;

namespace ProjectCore.Features.Snake.Components 
{
    public struct SnakeMoveDirection : IComponent
    {
        public Direction currentDirection;
    }
}