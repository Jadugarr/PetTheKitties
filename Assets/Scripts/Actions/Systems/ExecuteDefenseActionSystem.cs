using System.Collections.Generic;
using Entitas;

public class ExecuteDefenseActionSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1) {SubState.ExecuteAction};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1) {GameState.Battle};

    public ExecuteDefenseActionSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.ExecutionTime, GameMatcher.BattleAction));
    }

    protected override bool Filter(GameEntity entity)
    {
        if (entity.hasBattleAction)
        {
            return entity.battleAction.ActionType == ActionType.Defend && entity.executionTime.RemainingTime < 0f;
        }

        return false;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            GameEntity defendingCharacter = _context.GetEntityWithId(gameEntity.battleAction.EntityId);
            defendingCharacter.isDefend = true;

            gameEntity.isActionFinished = true;
        }
    }
}