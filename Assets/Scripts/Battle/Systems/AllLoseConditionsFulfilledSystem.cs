using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class AllLoseConditionsFulfilledSystem : GameReactiveSystem
{
    public AllLoseConditionsFulfilledSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.LoseConditionsFulfilled);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity != null && entity.isLoseConditionsFulfilled;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(SubState.PlayerLost);
    }
}