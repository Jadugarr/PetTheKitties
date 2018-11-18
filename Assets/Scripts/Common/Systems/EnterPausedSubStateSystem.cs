using System.Collections.Generic;
using Entitas;

public class EnterPausedSubStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Paused};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Undefined};

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

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        UIService.ShowWidget(AssetTypes.PauseOverlay, null);
    }
}