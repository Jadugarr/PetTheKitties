using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class ProcessPauseInputSystem : GameReactiveSystem
{
    public ProcessPauseInputSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Input);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.input.InputCommand == InputCommand.Pause && _context.isPauseInputAvailable;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(SubState.Paused);
    }
}