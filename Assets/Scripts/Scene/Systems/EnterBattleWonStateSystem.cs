using System.Collections.Generic;
using Entitas;

public class EnterBattleWonStateSystem : GameReactiveSystem
{
    public EnterBattleWonStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.PlayerWon;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.Battle &&
               _context.subState.CurrentSubState == SubState.PlayerWon;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (GameSystemService.GetSubSystemMapping(SubState.PlayerWon) == null)
        {
            CreatePlayerWonSystems();
        }
        
        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(SubState.PlayerWon));
    }

    private void CreatePlayerWonSystems()
    {
    }
}