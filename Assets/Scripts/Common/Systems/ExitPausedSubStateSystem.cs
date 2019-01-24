using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExitPausedSubStateSystem : GameReactiveSystem
{
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

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        UIService.HideWidget(UiAssetTypes.PauseOverlay);
        Time.timeScale = 1f;
    }
}