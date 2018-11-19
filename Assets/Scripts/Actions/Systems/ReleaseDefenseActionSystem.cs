using System.Collections.Generic;
using Entitas;

public class ReleaseDefenseActionSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1) {SubState.Waiting};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1) {GameState.Battle};

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

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            gameEntity.isDefend = false;
        }
    }
}