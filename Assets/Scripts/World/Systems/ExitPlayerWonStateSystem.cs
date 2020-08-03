using System.Collections.Generic;
using Entitas;

public class ExitPlayerWonStateSystem : GameReactiveSystem
{
    public ExitPlayerWonStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.SubState, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.PlayerWon;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        UIService.HideWidget(UiAssetTypes.BattleResultText);
    }
}