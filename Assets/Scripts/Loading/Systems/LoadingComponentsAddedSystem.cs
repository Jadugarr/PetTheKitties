using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

public class LoadingComponentsAddedSystem : GameReactiveSystem
{
    public LoadingComponentsAddedSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Loading, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.currentScene.Value != GameSceneConstants.LoadingScene;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.CreateEntity().AddChangeScene(GameSceneConstants.LoadingScene, LoadSceneMode.Additive);
    }
}