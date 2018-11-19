using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class ProcessUnpauseInputSystem : GameReactiveSystem
{
    public ProcessUnpauseInputSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Input);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.input.InputCommand == InputCommand.Unpause;
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