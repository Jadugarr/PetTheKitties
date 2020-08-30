using System.Collections.Generic;
using Entitas;

public class AllLoadingComponentsRemovedSystem : GameReactiveSystem
{
    private IGroup<GameEntity> loadingGroup;
    
    public AllLoadingComponentsRemovedSystem(IContext<GameEntity> context) : base(context)
    {
        loadingGroup = context.GetGroup(GameMatcher.Loading);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Loading, GroupEvent.Removed));
    }

    protected override bool Filter(GameEntity entity)
    {
        return loadingGroup.count == 0;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.isMasterLoadingActive = false;
        _context.CreateEntity().AddUnloadScene(GameSceneConstants.LoadingScene);
    }
}