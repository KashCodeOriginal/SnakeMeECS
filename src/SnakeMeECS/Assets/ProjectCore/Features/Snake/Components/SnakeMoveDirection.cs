using ME.ECS;
using UnityEngine.Serialization;

namespace ProjectCore.Features.Snake.Components 
{
    public struct SnakeMoveDirection : IComponent
    {
        [FormerlySerializedAs("CurrentDirection")] public Direction currentDirection;
    }
}