using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

public class LoadingComponentsAddedSystem : GameReactiveSystem
{
    private IGroup<GameEntity> loadingGroup;
    
    public LoadingComponentsAddedSystem(IContext<GameEntity> context) : base(context)
    {
        loadingGroup = context.GetGroup(GameMatcher.Loading);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Loading, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return !_context.isMasterLoadingActive;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.isMasterLoadingActive = true;
        _context.CreateEntity().AddChangeScene(GameSceneConstants.LoadingScene, LoadSceneMode.Additive);
    }
}