using System.Collections.Generic;
using Entitas;

public class ExitPausedSubStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Undefined};

    public ExitPausedSubStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.subState.PreviousSubState == SubState.Paused;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        UIService.HideWidget(AssetTypes.PauseOverlay);
    }
}