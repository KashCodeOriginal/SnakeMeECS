using ME.ECS;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace ProjectCore.Features.Snake.Components 
{
    public struct SnakePartSpawn : IComponent
    {
        public int3 BodySpawnPosition;
    }
}