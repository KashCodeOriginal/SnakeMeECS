using System.Collections.Generic;
using ME.ECS;
using Unity.Collections;

namespace ProjectCore.Features.Snake.Components
{
    public struct SnakeBody : IComponent
    {
        public NativeArray<SnakePart> SnakeParts;
    }
}