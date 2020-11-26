using System.Collections.Generic;
using System.Threading.Tasks;
using Configurations;
using Entitas;
using UnityEngine;

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
        Task.WaitAll(
            UIService.ShowWidget<AWidget>(GameConfigurations.AssetReferenceConfiguration.PauseOverlay, null),
            UIService.ShowWidget<AWidget>(GameConfigurations.AssetReferenceConfiguration.BattleResultWidget,
                new BattleResultWidgetProperties("Pause")));
        Time.timeScale = 0f;
    }
}