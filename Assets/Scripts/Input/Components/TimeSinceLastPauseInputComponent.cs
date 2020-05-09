using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game,Unique]
public class TimeSinceLastPauseInputComponent : IComponent
{
    public float Value;
}