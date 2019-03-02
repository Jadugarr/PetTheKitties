using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExitWorldStateSystem : GameReactiveSystem
{
    public ExitWorldStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.gameState.PreviousGameState == GameState.World;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.CreateEntity().AddUnloadScene(GameSceneConstants.WorldScene);
        GameSystemService.RemoveActiveSystems(GameSystemService.GetSystemMapping(GameState.World));
    }
}