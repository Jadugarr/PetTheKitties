using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class ProcessUnpauseInputSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Undefined};

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

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(_context.subState.PreviousSubState);
    }
}