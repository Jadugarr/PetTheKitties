using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Enums;
using Entitas.Extensions;

public class ActionFinishedSystem : GameReactiveSystem
{
    public ActionFinishedSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ActionFinished);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidStates()
    {
        return _context.battleState.CurrentBattleState == BattleState.ExecuteAction &&
               _context.gameState.CurrentGameState == GameState.Battle;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity actionEntity in entities)
        {
            actionEntity.ReplaceBattleAction(actionEntity.battleAction.EntityId, ActionType.ChooseAction,
                ActionATBType.Waiting);
            actionEntity.ReplaceExecutionTime(10f, 10f);
            actionEntity.RemoveTarget();
            actionEntity.isActionFinished = false;

            _context.SetNewBattlestate(BattleState.Waiting);
        }
    }
}