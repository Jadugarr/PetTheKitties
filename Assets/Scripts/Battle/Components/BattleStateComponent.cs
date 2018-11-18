using Entitas;
using Entitas.Battle.Enums;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class BattleStateComponent : IComponent
{
    public BattleState CurrentBattleState;
    public BattleState PreviousBattleState;
}