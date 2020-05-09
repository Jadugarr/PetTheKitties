using System.Collections.Generic;
using Entitas;

public class ResetTimeSinceLastJumpInputSystem : GameReactiveSystem
{
    public ResetTimeSinceLastJumpInputSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.input.InputCommand == InputCommand.Jump;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.ReplaceTimeSinceLastJumpInput(0f);
    }
}