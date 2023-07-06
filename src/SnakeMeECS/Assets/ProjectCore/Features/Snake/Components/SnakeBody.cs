using ME.ECS;
using UnityEngine.Serialization;

namespace ProjectCore.Features.Snake.Components {

    public struct SnakeBody : IComponent
    {
        public int ApplesEaten;
        public int Length;
    }
}