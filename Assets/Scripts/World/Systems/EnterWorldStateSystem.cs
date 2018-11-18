using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterWorldStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1) {SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1) {GameState.World};

    private IGroup<GameEntity> _sceneLoaded;

    public EnterWorldStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.gameState.CurrentGameState == GameState.World;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSystemMapping(GameState.World))
        {
            Debug.LogError("World systems haven't been created yet!");
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSystemMapping(GameState.World));

        _context.CreateEntity().AddChangeScene(GameSceneConstants.WorldScene, LoadSceneMode.Additive);
    }
}