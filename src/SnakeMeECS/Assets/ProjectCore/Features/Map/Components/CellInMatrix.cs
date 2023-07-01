﻿using ME.ECS;
using UnityEngine;

namespace ProjectCore.Features.Map.Components
{
    public struct CellInMatrix : IComponent
    {
        public Vector3 Position;
        public Entity? SnakePart;
        public Entity? Food;
    }
}