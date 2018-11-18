using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class WorldSceneLoadedSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1) {SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1) {GameState.World};

    public WorldSceneLoadedSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SceneLoaded);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.sceneLoaded.LoadedSceneName == GameSceneConstants.WorldScene;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        //Create player entity
        GameEntity playerEntity = _context.CreateEntity();
        playerEntity.isPlayer = true;
        playerEntity.AddMovementSpeed(5f);
        _context.SetNewSubstate(SubState.WorldNavigation);
    }
}