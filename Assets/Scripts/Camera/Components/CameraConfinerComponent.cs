using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class CameraConfinerComponent : IComponent
{
    public Collider2D Value;
}