using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class LoseConditionComponent : IComponent
{
    public ConditionModifier ConditionModifier;
    public LoseConditionState[] LoseConditions;
}

public struct LoseConditionState
{
    public LoseCondition LoseCondition;
    public bool IsFulfilled;
}