using System.Collections.Generic;
using Entitas;
using Entitas.Animations.Systems;
using Entitas.Common;
using Entitas.Input.Systems;
using Entitas.Kitty.Systems;
using Entitas.Position;
using Entitas.World.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterWorldStateSystem : GameReactiveSystem
{
    private IGroup<GameEntity> _sceneLoaded;

    public EnterWorldStateSystem(IContext<GameEntity> context) : base(context)
    {
        _sceneLoaded = _context.GetGroup(GameMatcher.SceneLoaded);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.gameState.CurrentGameState == GameState.World;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.CreateEntity().AddChangeScene(GameSceneConstants.WorldScene, LoadSceneMode.Additive);
    }
}