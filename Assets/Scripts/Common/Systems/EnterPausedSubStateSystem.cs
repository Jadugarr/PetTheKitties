using System.Collections.Generic;
using Entitas;

public class EnterPausedSubStateSystem : GameReactiveSystem
{
    public EnterPausedSubStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.subState.CurrentSubState == SubState.Paused;
    }

    protected override bool IsInValidState()
    {
        return _context.subState.CurrentSubState == SubState.Paused;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        UIService.ShowWidget(UiAssetTypes.PauseOverlay, null);
    }
}