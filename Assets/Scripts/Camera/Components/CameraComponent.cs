using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class CameraComponent : IComponent
{
    public Camera Camera;
}