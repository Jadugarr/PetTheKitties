using System.Collections.Generic;
using Entitas;

public class KittySavedSystem : GameReactiveSystem
{
    public KittySavedSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.GoalReached, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.ReplaceSavedKittyAmount(_context.savedKittyAmount.SavedKittyAmount + 1);
    }
}