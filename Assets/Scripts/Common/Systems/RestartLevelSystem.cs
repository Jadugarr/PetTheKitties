using System.Collections.Generic;
using Configurations;
using Entitas;
using Entitas.Extensions;
using Entitas.Unity;
using UnityEngine.AddressableAssets;

public class RestartLevelSystem : GameReactiveSystem
{
    private IGroup<GameEntity> levelEntities;
    public RestartLevelSystem(IContext<GameEntity> context) : base(context)
    {
        levelEntities = context.GetGroup(GameMatcher.Level);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.RestartLevel, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(SubState.WorldNavigation);
        GameEntity levelEntity = levelEntities.GetSingleEntity();
        levelEntity.isLoading = true;

        AssetReference nextLevelReference =
            GameConfigurations.AssetReferenceConfiguration.Levels[levelEntity.levelIndex.Value];
        Addressables.InstantiateAsync(nextLevelReference).Completed += handle =>
        {
            levelEntity.ReplaceView(handle.Result);
            handle.Result.Link(levelEntity);
            levelEntity.isLoading = false;
            _context.isRestartLevel = false;
        };
    }
}