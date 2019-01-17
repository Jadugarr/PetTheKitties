using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class InteractionTriggeredComponent : IComponent
{
    public int InteractedEntityId;
}