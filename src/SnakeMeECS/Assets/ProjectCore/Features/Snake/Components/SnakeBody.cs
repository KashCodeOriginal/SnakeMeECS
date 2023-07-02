using ME.ECS;
using UnityEngine.Serialization;

namespace ProjectCore.Features.Snake.Components {

    public struct SnakeBody : IComponent
    {
        [FormerlySerializedAs("Lenght")] public int Length;
    }
}