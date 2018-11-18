using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Unique]
public class SubStateComponent : IComponent
{
    public SubState PreviousSubState;
    public SubState CurrentSubState;
}