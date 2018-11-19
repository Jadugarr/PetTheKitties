using System.Collections.Generic;
using Entitas;

public class ChangeGameStateInputMapSystem : GameReactiveSystem
{
    public ChangeGameStateInputMapSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.PreviousGameState != entity.gameState.CurrentGameState;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            InputConfiguration.ChangeActiveGameStateInputMap(gameEntity.gameState.CurrentGameState);
        }
    }
}