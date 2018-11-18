using Entitas;
using Entitas.CodeGeneration.Attributes;
using Entitas.World;

[Game, Unique]
public class WorldStateComponent : IComponent
{
    public WorldState CurrentWorldState;
    public WorldState PreviousWorldState;
}