using Entitas;

[Game]
public class CollisionTriggerComponent : IComponent
{
    public int SourceEntityId;
    public int OtherEntityId;
}