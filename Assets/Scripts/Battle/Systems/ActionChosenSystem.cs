using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Enums;
using Entitas.Extensions;

public class ActionChosenSystem : GameReactiveSystem
{
    public ActionChosenSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.BattleAction, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.battleAction.ActionType != ActionType.None
            && entity.battleAction.ActionType != ActionType.ChooseAction;
    }

    protected override bool IsInValidStates()
    {
        return _context.battleState.CurrentBattleState == BattleState.ChooseAction &&
               _context.gameState.CurrentGameState == GameState.Battle;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewBattlestate(BattleState.ChooseTarget);
    }
}