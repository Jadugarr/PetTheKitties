using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Enums;

public class ReleaseDefenseActionSystem : GameReactiveSystem
{
    public ReleaseDefenseActionSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.BattleAction, GameMatcher.ExecutionTime));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isDefend && entity.executionTime.RemainingTime < 0f;
    }

    protected override bool IsInValidStates()
    {
        return _context.battleState.CurrentBattleState == BattleState.Waiting &&
               _context.gameState.CurrentGameState == GameState.Battle;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            gameEntity.isDefend = false;
        }
    }
}