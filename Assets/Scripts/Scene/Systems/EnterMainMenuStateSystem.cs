using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Common;
using UnityEngine.SceneManagement;

public class EnterMainMenuStateSystem : GameReactiveSystem
{
    public EnterMainMenuStateSystem(GameContext context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.CurrentGameState == GameState.MainMenu;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.MainMenu;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        GameEntity changeSceneEntity = _context.CreateEntity();
        changeSceneEntity.AddChangeScene(GameSceneConstants.MainMenuScene, LoadSceneMode.Additive);
    }
}