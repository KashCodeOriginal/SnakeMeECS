using ME.ECS;
using Unity.Mathematics;
using UnityEngine;

namespace ProjectCore.Features.Snake.Components 
{
    public struct SnakeInitializer : IComponent
    {
        public int2 SpawnSnakeMatrixPosition;
    }
}