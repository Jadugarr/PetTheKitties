using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class ProcessBattleCancelInputSystem : GameReactiveSystem
{
    public ProcessBattleCancelInputSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Input);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity != null && entity.input != null && entity.input.InputCommand == InputCommand.CancelAction;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(_context.subState.PreviousSubState);
    }
}