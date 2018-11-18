using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class WinConditionComponent : IComponent
{
    public ConditionModifier ConditionModifier;
    public WinConditionState[] WinConditions;
}

public struct WinConditionState
{
    public bool IsFulfilled;
    public WinCondition WinCondition;
}