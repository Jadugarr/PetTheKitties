using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine.SceneManagement;

[Game, Unique]
public class CurrentSceneComponent : IComponent
{
    public string Value;
}