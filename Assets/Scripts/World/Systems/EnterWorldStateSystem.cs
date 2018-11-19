using System.Collections.Generic;
using Entitas;
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
        _sceneLoaded.OnEntityAdded += OnWorldSceneLoaded;

        _context.CreateEntity().AddChangeScene(GameSceneConstants.WorldScene, LoadSceneMode.Additive);
    }

    private void OnWorldSceneLoaded(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        if (!GameSystemService.HasSystemMapping(GameState.World))
        {
            Debug.LogError("World systems haven't been created yet!");
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSystemMapping(GameState.World));
    }
}