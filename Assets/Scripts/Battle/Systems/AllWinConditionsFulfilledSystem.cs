using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class AllWinConditionsFulfilledSystem : GameReactiveSystem
{
    public AllWinConditionsFulfilledSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.WinConditionsFulfilled);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity != null && entity.isWinConditionsFulfilled;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(SubState.PlayerWon);
    }
}